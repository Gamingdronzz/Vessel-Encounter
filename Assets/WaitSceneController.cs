using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VesselEncounter.Data;
using Photon.Pun;
using Photon.Realtime;
using System;

namespace VesselEncounter
{
    public class WaitSceneController : MonoBehaviour
    {
        private GameObject Player;
        private WaitForSeconds seconds = new WaitForSeconds(1f);
        public TextMeshProUGUI CountDown, PlayerCount, MyPlayerName;

        // Use this for initialization
        private void Start()
        {
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.WaitingScene);
            int countdownValue = GameData.Instance.MatchWaitTime;
            XDebug.Log("New Wait Time = " + countdownValue);
            if (CountDown != null)
                StartCoroutine(StartCountdown(countdownValue));
            Player = PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            GameData.Instance.PlayerGO = Player;
            OnJoinedRoom(PhotonNetwork.LocalPlayer);
            UpdatePlayerCount();
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void UpdatePlayerCount()
        {
            PlayerCount.text = "Players in Room\n" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnPlayerJoined.EventActionVoid += UpdatePlayerCount;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid += UpdatePlayerCount;
            MyEventManager.Instance.OnGamePlayConditionsMet.EventActionVoid += OnGamePlayConditionsMet;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnPlayerJoined.EventActionVoid -= UpdatePlayerCount;
            MyEventManager.Instance.OnPlayerLeft.EventActionVoid -= UpdatePlayerCount;
            MyEventManager.Instance.OnGamePlayConditionsMet.EventActionVoid -= OnGamePlayConditionsMet;
        }

        private IEnumerator StartCountdown(int value)
        {
            for (int i = value; i > 0; i--)
            {
                CountDown.text = i.ToString();
                yield return seconds;
            }
            MyEventManager.Instance.OnGamePlayConditionsMet.Dispatch();
        }

        public void OnJoinedRoom(Player player)
        {
            MyPlayerName.text = player.NickName + " == " + player.UserId;
        }

        private void OnGamePlayConditionsMet()
        {
        }
    }
}