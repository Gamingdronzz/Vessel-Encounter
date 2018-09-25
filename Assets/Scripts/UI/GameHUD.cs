using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace VesselEncounter.UI
{
    public class GameHUD : SingletonMonoBehaviour<GameHUD>
    {
        //public static UI INSTANCE;

        public RawImage CompassImage;
        public RawImage CrosshairImage;

        public GameObject HUDPanelGO;

        [SerializeField]
        private TextMeshProUGUI PlayerName;

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

        private void OnEnable()
        {
            MyEventManager.Instance.OnPlayerNameChanged.EventActionString += SetPlayerName;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnPlayerNameChanged.EventActionString -= SetPlayerName;
        }

        private void Reset()
        {
            //CompassImage = GameObject.Find("CompassImage").GetComponent<RawImage>();
            //CrosshairImage = GameObject.Find("CrosshairImage").GetComponent<RawImage>();

            //HUDPanelGO = GameObject.Find("HUDPanel");
        }

        public void SetPlayerName(string playerName)
        {
            XDebug.Log("Setting Player Name", XDebug.Mask.GameHUD);
            PlayerName.text = playerName;
        }
    }
}