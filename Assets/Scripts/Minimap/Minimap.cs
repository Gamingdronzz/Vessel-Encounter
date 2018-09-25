using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class Minimap : SingletonMonoBehaviour<Minimap>
    {
        //public static Minimap INSTANCE;
        public Transform ShipTransform;

        private Transform m_MyTransform;

        //private void Awake()
        //{
        //    if (INSTANCE == null)
        //    {
        //        INSTANCE = this;
        //    }
        //    else if (INSTANCE != this)
        //    {
        //        Destroy(this);
        //    }
        //}

        // Use this for initialization
        private void Start()
        {
            m_MyTransform = transform;
        }

        // Update is called once per frame
        private void LateUpdate()
        {
            Vector3 newPos = ShipTransform.position;
            newPos.y = m_MyTransform.position.y;
            m_MyTransform.position = newPos;

            m_MyTransform.rotation = Quaternion.Euler(90.0f, ShipTransform.eulerAngles.y, 0.0f);
        }
    }
}