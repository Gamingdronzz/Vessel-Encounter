using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace VesselEncounter { 

public class APIRequest :SingletonMonoBehaviour<APIRequest> {

        private const string API_BASE = "";
        private string api;

        public void setAPI(string end) {
            api = API_BASE + end;
        }

        public string makeRequest()
        {
            HttpWebRequest request =(HttpWebRequest)WebRequest.Create(api);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string jsonResponse = reader.ReadToEnd();
            return jsonResponse;
        }
    }
}
