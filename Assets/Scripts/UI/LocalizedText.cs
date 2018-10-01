using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VesselEncounter
{
    public class LocalizedText : MonoBehaviour
    {
        public string key;

        void Start()
        {
            TextMeshProUGUI textMesh = this.gameObject.GetComponent<TextMeshProUGUI>();
            textMesh.text = LocalizationManager.Instance.GetLocalizedValue(key);
        }

    }
}