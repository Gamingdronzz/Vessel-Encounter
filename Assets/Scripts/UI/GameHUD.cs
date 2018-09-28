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
        private TextMeshProUGUI PlayerList;

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
            //MyEventManager.Instance.OnPlayerNameChanged.EventActionString += SetPlayerName;
            MyEventManager.Instance.OnGameStateUpdated.EventActionVoid += ListAllPlayers;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid += ListAllPlayers;
        }

        private void OnDisable()
        {
            //MyEventManager.Instance.OnPlayerNameChanged.EventActionString -= SetPlayerName;
            MyEventManager.Instance.OnGameStateUpdated.EventActionVoid -= ListAllPlayers;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid -= ListAllPlayers;
        }

        private void ListAllPlayers()
        {
            if (GameStateManager.Instance.GetCurrentGameState() == GameStateManager.GameState.Game)
            {
                string s = "Players in Room: ";
                foreach (Player p in PhotonNetwork.CurrentRoom.Players.Values)
                {
                    s = s + p.NickName + ",";
                }
                PlayerList.text = s.Substring(0, s.Length - 1);
            }
        }

        private void Reset()
        {
            //CompassImage = GameObject.Find("CompassImage").GetComponent<RawImage>();
            //CrosshairImage = GameObject.Find("CrosshairImage").GetComponent<RawImage>();

            //HUDPanelGO = GameObject.Find("HUDPanel");
        }

        //public void SetPlayerName(string playerName)
        //{
        //    XDebug.Log("Setting Player Name", XDebug.Mask.GameHUD);
        //    PlayerName.text = playerName;
        //}
    }
}