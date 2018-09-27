using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace VesselEncounter.Networking
{
    public class MyWebRequest : SingletonMonoBehaviour<MyWebRequest>
    {
        public enum RequestType
        {
            GET,
            POST
        }

        private enum RequestState
        {
            INIT,
            PROCESSED,
            FAILURE,
            SUCCESS
        }

        public void MakeWebRequest(string url, Action<string> OnSuccess, Action<string> OnFailure, string bodyParams = "", RequestType requestType = RequestType.GET)
        {
            if (requestType == RequestType.POST)
            {
                StartCoroutine(PostRequest(url, bodyParams, OnSuccess, OnFailure));
            }
            else
            {
                StartCoroutine(GetRequest(url, OnSuccess, OnFailure));
            }
        }

        public IEnumerator GetRequest(string uri, Action<string> OnSuccess, Action<string> OnFailure)
        {
            RequestState requestState = RequestState.INIT;

            UnityWebRequest request = UnityWebRequest.Get(uri);
            yield return request.SendWebRequest();

            requestState = RequestState.PROCESSED;
            if (request.isNetworkError)
            {
                requestState = RequestState.FAILURE;
            }
            else
            {
                if (request.responseCode == 200)
                {
                    requestState = RequestState.SUCCESS;
                }
                else if (request.responseCode == 401) // an occasional unauthorized error
                {
                    requestState = RequestState.FAILURE;
                    StartCoroutine(GetRequest(uri, OnSuccess, OnFailure));
                }
                else
                {
                    requestState = RequestState.FAILURE;
                }

                ProcessResult(requestState, request, OnSuccess, OnFailure);
            }
        }

        private void ProcessResult(RequestState requestState, UnityWebRequest request, Action<string> OnSuccess, Action<string> OnFailure)
        {
            if (requestState == RequestState.FAILURE)
            {
                XDebug.LogError(request.error, XDebug.Mask.MyWebRequest);
                OnFailure(request.error);
            }
            else if (requestState == RequestState.SUCCESS)
            {
                XDebug.Log("API Response", XDebug.Mask.MyWebRequest, XDebug.Color.Blue);
                XDebug.Log(request.downloadHandler.text, XDebug.Mask.MyWebRequest, XDebug.Color.Blue);
                OnSuccess(request.downloadHandler.text);
            }
        }

        public IEnumerator PostRequest(string url, string bodyJsonString, Action<string> OnSuccess, Action<string> OnFailure)
        {
            RequestState requestState = RequestState.INIT;
            var request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(bodyJsonString);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();
            requestState = RequestState.PROCESSED;

            if (request.isNetworkError)
            {
                requestState = RequestState.FAILURE;
            }
            else
            {
                if (request.responseCode == 201)
                {
                    requestState = RequestState.SUCCESS;
                }
                else if (request.responseCode == 401)
                {
                    requestState = RequestState.FAILURE;
                    StartCoroutine(PostRequest(url, bodyJsonString, OnSuccess, OnFailure));
                }
                else
                {
                    requestState = RequestState.FAILURE;
                }
            }

            ProcessResult(requestState, request, OnSuccess, OnFailure);
        }
    }
}