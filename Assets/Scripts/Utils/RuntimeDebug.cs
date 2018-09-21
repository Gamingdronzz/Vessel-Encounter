using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VesselEncounter
{
    public class RuntimeDebug : SingletonMonoBehaviour<RuntimeDebug>
    {
        public TextMeshProUGUI Log;
        public Transform parentTransform;

        public void AddLog(string message)
        {
            Log.text = message;
            Instantiate(Log, parentTransform);
        }
    }
}