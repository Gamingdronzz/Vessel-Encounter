using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace VesselEncounter.Networking
{
    public class MyWebRequest : SingletonMonoBehaviour<MyWebRequest>
    {
        private bool requestFinished;
        private bool requestErrorOccurred;

        public enum RequestType
        {
            GET,
            POST
        }

        public void MakeWebRequest(string url, Action<string> action, string bodyParams = "", RequestType requestType = RequestType.POST)
        {
            if (requestType == RequestType.POST)
            {
                PostRequest(url, bodyParams, action);
            }
            else
            {
                GetRequest(url, action);
            }
        }

        public IEnumerator GetRequest(string uri, Action<string> action)
        {
            requestFinished = false;
            requestErrorOccurred = false;

            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();

            requestFinished = true;
            if (request.isNetworkError)
            {
                Debug.Log("Something went wrong, and returned error: " + request.error);
                requestErrorOccurred = true;
            }
            else
            {
                // Show results as text
                Debug.Log(request.downloadHandler.text);

                if (request.responseCode == 200)
                {
                    Debug.Log("Request finished successfully!");
                }
                else if (request.responseCode == 401) // an occasional unauthorized error
                {
                    Debug.Log("Error 401: Unauthorized. Resubmitted request!");
                    StartCoroutine(GetRequest(uri, action));
                    requestErrorOccurred = true;
                }
                else
                {
                    Debug.Log("Request failed (status:" + request.responseCode + ")");
                    requestErrorOccurred = true;
                }

                if (!requestErrorOccurred)
                {
                    yield return null;
                    // process results
                    action(request.downloadHandler.text);
                }
            }
        }

        public IEnumerator PostRequest(string url, string bodyJsonString, Action<string> action)
        {
            requestFinished = false;
            requestErrorOccurred = false;

            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();
            requestFinished = true;

            if (request.isNetworkError)
            {
                Debug.Log("Something went wrong, and returned error: " + request.error);
                requestErrorOccurred = true;
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);

                if (request.responseCode == 201)
                {
                    Debug.Log("Request finished successfully! New User created successfully.");
                }
                else if (request.responseCode == 401)
                {
                    Debug.Log("Error 401: Unauthorized. Resubmitted request!");
                    StartCoroutine(PostRequest(url, bodyJsonString, action));
                    requestErrorOccurred = true;
                }
                else
                {
                    Debug.Log("Request failed (status:" + request.responseCode + ").");
                    requestErrorOccurred = true;
                }

                if (!requestErrorOccurred)
                {
                    yield return null;
                    // process results
                }
            }
        }
    }
}