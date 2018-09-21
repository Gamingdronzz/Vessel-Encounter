using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.Data;
using TMPro;
using System;

namespace VesselEncounter.UI.MainMenu
{
    public class MainMenu : SingletonMonoBehaviour<MainMenu>
    {
        public TMP_Dropdown RegionListDropdown;

        public TMP_Dropdown.DropdownEvent OnDropDownValueChanged { get; private set; }
        private int index;

        private void Start()
        {
            this.gameObject.SetActive(true);
            OnDropDownValueChanged = new TMP_Dropdown.DropdownEvent();
        }

        public void UpdateRegionList(List<Region> regions)
        {
            List<string> options = new List<string>();

            foreach (Region region in regions)
            {
                options.Add(region.Code);
            }
            RegionListDropdown.ClearOptions();
            RegionListDropdown.AddOptions(options);
            RegionListDropdown.onValueChanged = OnDropDownValueChanged;
            OnDropDownValueChanged.AddListener(OnRegionSelected);
            XDebug.Log("Updating region list");
        }

        private void OnRegionSelected(int index)
        {
            string Region = NetworkData.Instance.GetRegionList()[index].Code;
            //PhotonNetwork.Disconnect();
            //PhotonNetwork.ConnectToRegion(Region);
            XDebug.Log("Connecting to - " + Region);
        }

        public void StartMatchMaking()
        {
            GameData.Instance.MaxPlayers = 2;
            GameData.Instance.MatchWaitTime = 30;
            GameManager.Instance.CreateOrJoinRoom();
        }

        public void OnConnectedToBestRegion()
        {
            RegionListDropdown.value = NetworkData.Instance.GetBestRegionIndex();
        }
    }
}