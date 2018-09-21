using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.Data;

namespace VesselEncounter.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        private void Start()
        {
            this.gameObject.SetActive(true);
        }

        public void StartMatchMaking()
        {
            GameData.Instance.MaxPlayers = 2;
            GameData.Instance.MatchWaitTime = 30;
            GameManager.Instance.CreateOrJoinRoom();
        }
    }
}