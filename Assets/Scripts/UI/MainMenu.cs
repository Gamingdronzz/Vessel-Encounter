using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            GameManager.Instance.CreateOrJoinRoom(30, 2);
        }
    }
}