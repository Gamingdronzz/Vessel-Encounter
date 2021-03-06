﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.Data;
using TMPro;
using System;
using UnityEngine.EventSystems;
using VesselEncounter.Networking;

namespace VesselEncounter.UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        public TMP_Dropdown RegionListDropdown;
        public GameObject PlayerNameInput;
        public TMP_InputField PlayerName;

        public TMP_Dropdown.DropdownEvent OnDropDownValueChanged { get; private set; }
        private int index;

        private void Start()
        {
            this.gameObject.SetActive(true);
            OnDropDownValueChanged = new TMP_Dropdown.DropdownEvent();
            InputManager.Instance.ActivateInput(false);
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnRegionListUpdated.AddListener(OnRegionListUpdated);
            MyEventManager.Instance.OnConnectedToBestRegion.AddListener(OnConnectedToBestRegion);
        }

        private void OnDisable()
        {
            try
            {
                MyEventManager.Instance.OnRegionListUpdated.RemoveListener(OnRegionListUpdated);
                MyEventManager.Instance.OnConnectedToBestRegion.RemoveListener(OnConnectedToBestRegion);
            }
            catch (NullReferenceException nre)
            {
                XDebug.Log(nre.StackTrace, XDebug.Mask.MainMenu);
            }
        }

        public void OnRegionListUpdated()
        {
            XDebug.Log("Updating Region List", XDebug.Mask.MainMenu, XDebug.Color.Yellow);
            List<string> options = new List<string>();

            foreach (Region region in NetworkData.Instance.GetRegionList())
            {
                options.Add(region.Code + " - " + (region.Ping / 10000000) + " ms");
            }
            RegionListDropdown.ClearOptions();
            RegionListDropdown.AddOptions(options);
            RegionListDropdown.onValueChanged = OnDropDownValueChanged;
            OnDropDownValueChanged.AddListener(OnRegionSelected);
        }

        private void OnRegionSelected(int index)
        {
            NetworkData.Instance.UserChoiceRegion = NetworkData.Instance.GetRegionList()[index].Code;
            PhotonNetwork.Disconnect();
            //PhotonNetwork.ConnectToRegion(Region);
            XDebug.Log("Connecting to - " + NetworkData.Instance.UserChoiceRegion);
        }

        public void ActivateNameInputWindow()
        {
            PlayerNameInput.SetActive(true);
        }

        public void StartMatchMaking()
        {
            PlayerNameInput.SetActive(false);
            PhotonNetwork.NickName = PlayerName.text;
            GameData.Instance.MaxPlayers = 4;
            GameData.Instance.MatchWaitTime = 30;
            GameManager.Instance.CreateOrJoinRoom();
        }

        public void OnConnectedToBestRegion()
        {
            RegionListDropdown.value = NetworkData.Instance.GetBestRegionIndex();
            InputManager.Instance.ActivateInput(true);

            MyWebRequest.Instance.MakeWebRequest("https://jsonplaceholder.typicode.com/todos/1", OnSuccess, OnFailure, "");
            //StartCoroutine(MyWebRequest.Instance.GetRequest("https://jsonplaceholder.typicode.com/todos/1"));
        }

        private void OnFailure(string error)
        {
        }

        private void OnSuccess(string response)
        {
        }
    }
}