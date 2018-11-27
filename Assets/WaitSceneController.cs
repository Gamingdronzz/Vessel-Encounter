using UnityEngine;
using TMPro;
using VesselEncounter.Data;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace VesselEncounter
{
    public class WaitSceneController : MonoBehaviour
    {
        private GameObject Player;
        private WaitForSeconds seconds = new WaitForSeconds(1f);
        public TextMeshProUGUI CountDown, PlayerCount, PlayerList;

        // Use this for initialization
        private void Start()
        {
            PhotonNetwork.FetchServerTimestamp();
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.WaitingScene);
            Player = PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            GameData.Instance.PlayerGO = Player;
            UpdatePlayerCountAndList();
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
                    hashtable[RoomPropertyKeys.Key_MatchWaitTime] = i - 1;
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

        private void UpdatePlayerCountAndList()
        {
            ArrayList players = new ArrayList();
            PlayerCount.text = "Players in Room\n" + PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;
            PlayerList.text = "";
            foreach (KeyValuePair<int, Player> keyValPair in PhotonNetwork.CurrentRoom.Players.OrderBy(i => i.Key))
            {
                PlayerList.text = PlayerList.text + keyValPair.Value.NickName + " Joined\n";
            }
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnPlayerJoined.AddListener(UpdatePlayerCountAndList);
            MyEventManager.Instance.OnPlayerLeft.AddListener(UpdatePlayerCountAndList);
            MyEventManager.Instance.OnGamePlayConditionsMet.AddListener(OnGamePlayConditionsMet);
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnPlayerJoined.RemoveListener(UpdatePlayerCountAndList);
            MyEventManager.Instance.OnPlayerLeft.RemoveListener(UpdatePlayerCountAndList);
            MyEventManager.Instance.OnGamePlayConditionsMet.RemoveListener(OnGamePlayConditionsMet);
        }

        private void OnGamePlayConditionsMet()
        {
        }
    }
}