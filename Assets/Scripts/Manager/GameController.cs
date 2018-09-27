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
            MyEventManager.Instance.OnPlayerNameChanged.Dispatch(PhotonNetwork.LocalPlayer.NickName.ToString());
            GameStateManager.Instance.UpdateGameState(GameStateManager.GameState.Game);
        }
    }
}