using UnityEngine;
using TMPro;
using VesselEncounter.Data;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;

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
            PhotonNetwork.FetchServerTimestamp();
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.WaitingScene);
            Player = PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            GameData.Instance.PlayerGO = Player;
            OnJoinedRoom(PhotonNetwork.LocalPlayer);
            UpdatePlayerCount();
            StartCoroutine(StartCountdown((int)PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKeys.Key_MatchWaitTime]));
        }

        private IEnumerator StartCountdown(int value)
        {
            for (int i = value; i > 0; i--)
            {
                CountDown.text = i.ToString();
                if (PhotonNetwork.IsMasterClient)
                {
                    ExitGames.Client.Photon.Hashtable hashtable = PhotonNetwork.CurrentRoom.CustomProperties;
                    hashtable[RoomPropertyKeys.Key_MatchWaitTime] = i;
                    PhotonNetwork.CurrentRoom.SetCustomProperties(hashtable);
                }
                yield return seconds;
            }
            MyEventManager.Instance.OnGamePlayConditionsMet.Dispatch();
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

        public void OnJoinedRoom(Player player)
        {
            MyPlayerName.text = player.NickName + " == " + player.UserId;
        }

        private void OnGamePlayConditionsMet()
        {
        }
    }
}