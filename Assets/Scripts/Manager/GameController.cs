using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.Data;

namespace VesselEncounter.Game
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            XDebug.Log("Loading Game Scene .Players in room = " + PhotonNetwork.CurrentRoom.PlayerCount);
            GameObject Player = PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            GameData.Instance.PlayerGO = Player;
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.Game);
            MyEventManager.Instance.OnPlayerNameChanged.Dispatch(PhotonNetwork.LocalPlayer.NickName.ToString());
            XDebug.Log("My Player Name = " + PhotonNetwork.LocalPlayer.NickName + " ID = " + PhotonNetwork.LocalPlayer.UserId);
        }
    }
}