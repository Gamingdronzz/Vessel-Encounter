using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter.Game
{
    public class GameController : MonoBehaviour
    {
        private void Start()
        {
            PhotonNetwork.Instantiate("Player_Ship", new Vector3(0, 0, 0), Quaternion.identity, 0);
            MyEventManager.Instance.OnPlayerNameChanged.Dispatch(PhotonNetwork.LocalPlayer.NickName);
        }
    }
}