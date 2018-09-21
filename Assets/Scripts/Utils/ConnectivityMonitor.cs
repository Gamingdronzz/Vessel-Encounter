using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace VesselEncounter
{
    public class ConnectivityMonitor : MonoBehaviour
    {
        public enum Status
        {
            PendingVerification,    // Internet access is being verified
            Offline,                // No internet access
            Online                  // Internet access is verified and functional
        }

        public bool ForceInternetDown = false; //For testing purpose only.
        private float m_UpdateFrequency = 5f;
        public int MaxRetries = 2;

        //We hit both the URL`s one by one. We start with first URL(this should be pointing to CDN), If first URL fails, we hit the second URL, which is an API.
        //Both URL`s return "true" as response
        public string[] RemoteURL = null;

        private Status m_Status = Status.PendingVerification;
        private bool m_IsChecking = false;
        private float m_Timer = 0f;
        private int m_RetryCount = 0;
        private int m_Index = 0;

        #region Static Methods

        private static System.Action<Status> OnInternetStateChange = null;
        private static ConnectivityMonitor mInstance = null;

        public static bool pIsInternetAvailable
        {
            get
            {
                if (mInstance != null)
                {
                    if (mInstance.ForceInternetDown)
                        return false;
                    return pStatus == Status.Online;
                }
                return true;
            }
        }

        public static Status pStatus
        {
            get
            {
                if (mInstance != null)
                    return mInstance.m_Status;
                return Status.Offline;
            }
            private set
            {
                if (mInstance.m_Status != value)
                {
                    mInstance.m_Status = value;
                    if (mInstance.m_Status == Status.Online)
                        mInstance.m_RetryCount = 0;

                    if (OnInternetStateChange != null)
                        OnInternetStateChange(mInstance.m_Status);
                }
            }
        }

        public static void SetInternetState(bool isDown)
        {
            if (mInstance != null)
                mInstance.ForceInternetDown = isDown;
        }

        public static void AddListener(Action<Status> callback)
        {
            if (callback != null)
            {
                OnInternetStateChange += callback;
                if (mInstance != null)
                    callback(pStatus);
                else
                    callback(Status.PendingVerification);
            }
        }

        public static void RemoveListener(Action<Status> callback)
        {
            if (callback != null)
                OnInternetStateChange -= callback;
        }

        public static void ForceCheckConnection()
        {
            if (mInstance != null)
                mInstance.m_Timer = 0;
        }

        public static void Init()
        {
            if (mInstance != null)
            {
                pStatus = mInstance.GetUnityStatus();
                if (mInstance.RemoteURL.Length > 0)
                {
                    mInstance.enabled = true;
                }
                else
                {
                    XDebug.Log("Remote URLs are empty, Checking for Unity status, status : " + pStatus, XDebug.Mask.ConnectivityMonitor, XDebug.Color.Yellow);
                    pStatus = mInstance.GetUnityStatus();
                }
            }
            else
                XDebug.Log("ConnectivityMonitor instance is null", XDebug.Mask.ConnectivityMonitor, XDebug.Color.Red);
        }

        #endregion Static Methods

        #region Private Methods

        private void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this;
                DontDestroyOnLoad(gameObject);
                m_UpdateFrequency = RemoteSettings.GetFloat("NetConnectCheckFrequency", 5f);
                RemoteSettings.Updated += new RemoteSettings.UpdatedEventHandler(HandleRemoteUpdate);
                if (RemoteURL.Length == 0)
                {
                    enabled = false;
                    pStatus = GetUnityStatus();
                }
                enabled = false;
            }
            else
                Destroy(gameObject);
        }

        private void HandleRemoteUpdate()
        {
            m_UpdateFrequency = RemoteSettings.GetFloat("NetConnectCheckFrequency", 5f);
            UnityEngine.Debug.Log("NetConnectCheckFrequency " + m_UpdateFrequency);
        }

        private void Update()
        {
            Status status = GetUnityStatus();
            if (status == Status.Offline)
            {
                pStatus = status;
            }
            else
            {
                if (!m_IsChecking && m_Index == 0)
                {
                    m_Timer -= Time.unscaledDeltaTime;
                    if (m_Timer <= 0)
                        StartCoroutine(CheckConnection(RemoteURL[m_Index]));
                }
            }
        }

        private IEnumerator CheckConnection(string url)
        {
            m_IsChecking = true;
            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                request.timeout = 30;
                yield return request.SendWebRequest();
                m_IsChecking = false;
                m_Timer = m_UpdateFrequency;
                if (request.isNetworkError || request.isHttpError || ForceInternetDown)
                {
                    UnityEngine.Debug.LogErrorFormat("Response URL : {0}, Error : {1}, Response Code : {2}", request.url, request.error, request.responseCode);
                    m_RetryCount++;
                    if (m_RetryCount >= MaxRetries)
                        OnConnectionVerified(false);
                }
                else
                {
                    XDebug.Log("Response URL : " + request.url + "\nText : " + request.downloadHandler.text + "\nResponse Code : " + request.responseCode, XDebug.Mask.ConnectivityMonitor, XDebug.Color.Yellow);

                    //Call succeeded, now check if the data returned is what we expect.
                    if (request.downloadHandler.text.Equals("true") || request.downloadHandler.text.Equals("true\n"))
                        OnConnectionVerified(true);
                    else
                    {
                        //The call was successfull, but content was wrong, this happens in case of ISP login.
                        m_RetryCount++;
                        if (m_RetryCount >= MaxRetries)
                            OnConnectionVerified(false);
                    }
                }
            }
        }

        private void OnConnectionVerified(bool isConnected)
        {
            if (isConnected)
            {
                m_Index = 0;
                pStatus = Status.Online;
            }
            else
            {
                ++m_Index;
                if (m_Index >= RemoteURL.Length)
                {
                    m_Index = 0;
                    pStatus = Status.Offline;
                }
                else
                    StartCoroutine(CheckConnection(RemoteURL[m_Index]));
            }
        }

        private void OnApplicationFocus(bool focus)
        {
            //If we just got focus, check for network right away
            if (focus)
                ForceCheckConnection();
        }

        /// <summary>
        /// Gets the network status by using unity`s API.
        /// </summary>
        /// <returns>The network status.</returns>
        private Status GetUnityStatus()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
                return Status.Offline;
            return Status.Online;
        }

        #endregion Private Methods
    }
}