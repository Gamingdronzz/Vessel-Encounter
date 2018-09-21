using System;
using UnityEngine;
using TMPro;
using ExitGames.Client.Photon;
using System.Collections;
using Photon.Pun;

namespace VesselEncounter.Game
{
    public class GameCanvas : MonoBehaviour
    {
        private WaitForSeconds seconds = new WaitForSeconds(1f);
        public TextMeshProUGUI TxtTimer;
        private object matchCreationTime;

        // Use this for initialization
        private void Start()
        {
            int countdownValue = 30;

            if (TxtTimer != null)
                StartCoroutine(StartCountdown(countdownValue));
        }

        private IEnumerator StartCountdown(int value)
        {
            //GameData.Instance.currentRoom.CustomProperties.TryGetValue("ServerTimeStamp", out matchCreationTime);
            //XDebug.Log("Time Since Match Creation = " + ((double)matchCreationTime - PhotonNetwork.Time));
            for (int i = value; i > 0; i--)
            {
                TxtTimer.text = i.ToString();
                yield return seconds;
                //if (i == 20)
                //    XDebug.Log("Time Since Match Creation After 10 seconds = " + ((double)matchCreationTime - PhotonNetwork.ServerTimestamp));
            }
        }

        // Update is called once per frame
        private void Update()
        {
        }
    }
}