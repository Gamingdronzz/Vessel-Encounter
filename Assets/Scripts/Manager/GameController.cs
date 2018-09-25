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
            XDebug.Log("Loading Game Scene .Players in room = " + NetworkData.Instance.CurrentRoom.PlayerCount);
            GameObject Player = PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            GameData.Instance.PlayerGO = Player;
            Player.name = PhotonNetwork.LocalPlayer.NickName;
            MyEventManager.Instance.OnPlayerNameChanged.Dispatch(PhotonNetwork.LocalPlayer.NickName.ToString());
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.Game);
        }
    }
}