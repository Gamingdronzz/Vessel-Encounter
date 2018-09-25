using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VesselEncounter.UI {
    public class GameHUD : SingletonMonoBehaviour<GameHUD>
    {
        //public static UI INSTANCE;

        public RawImage CompassImage;
        public RawImage CrosshairImage;

        public GameObject HUDPanelGO;

        // Use this for initialization
        private void Start()
        {
            //if (INSTANCE == null)
            //{
            //    INSTANCE = this;
            //}
            //else if (INSTANCE != this)
            //{
            //    Destroy(this);
            //}
        }

        private void Reset()
        {
            //CompassImage = GameObject.Find("CompassImage").GetComponent<RawImage>();
            //CrosshairImage = GameObject.Find("CrosshairImage").GetComponent<RawImage>();

            //HUDPanelGO = GameObject.Find("HUDPanel");
        }
    }
}