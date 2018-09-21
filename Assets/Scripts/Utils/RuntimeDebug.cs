using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace VesselEncounter
{
    public class RuntimeDebug : SingletonMonoBehaviour<RuntimeDebug>
    {
        public TextMeshProUGUI Log;

        public void AddLog(string message)
        {
            Log.text += "\n" + message;
        }
    }
}