using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.UI;

namespace VesselEncounter
{
    public class ShipCompass : MonoBehaviour
    {
        private Transform m_MyTransform;

        // Use this for initialization
        private void Start()
        {
            m_MyTransform = transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            //if (m_MyTransform != null && GameHUD.Instance.CompassImage != null)
            //    GameHUD.Instance.CompassImage.uvRect = new Rect(m_MyTransform.localEulerAngles.y / 360, 0, 1, 1);
            //else
            //    XDebug.Log("Transform or gameHUD instance null", XDebug.Mask.ShipCompass);
        }
    }
}