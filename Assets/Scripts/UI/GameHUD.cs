using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Photon.Pun;
using Photon.Realtime;

namespace VesselEncounter.UI
{
    public class GameHUD : SingletonMonoBehaviour<GameHUD>
    {
        //public static UI INSTANCE;

        public RawImage CompassImage;
        public RawImage CrosshairImage;

        public GameObject HUDPanelGO;

        [SerializeField]
        private TextMeshProUGUI PlayerName, OtherPlayerList;

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
            MyEventManager.Instance.OnGameStateUpdated.EventActionVoid += ListOtherPlayers;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid += ListOtherPlayers;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnPlayerNameChanged.EventActionString -= SetPlayerName;
            MyEventManager.Instance.OnGameStateUpdated.EventActionVoid -= ListOtherPlayers;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid -= ListOtherPlayers;
        }

        private void ListOtherPlayers()
        {
            string s = "Other Players: ";
            foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
            {
                if (!p.IsLocal)
                    s = s + p.NickName + ",";
            }
            OtherPlayerList.text = s.Substring(0, s.Length - 1);

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