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
        void Start()
        {
            m_MyTransform = transform;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            GameHUD.Instance.CompassImage.uvRect = new Rect(m_MyTransform.localEulerAngles.y / 360, 0, 1, 1);
        }
    }
}
