using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.Data;

namespace VesselEncounter
{
    public class CanvasMatchList : MonoBehaviour
    {
        public GameObject MatchItemTemplate;
        public Transform parentTransform;

        // Use this for initialization
        private void Start()
        {
            this.gameObject.SetActive(false);
        }

        public void UpdateUI()
        {
            Debug.Log("Updating UI");
            if (NetworkData.Instance.RoomList != null && NetworkData.Instance.RoomList.Count > 0)
            {
                int i = 1;
                foreach (RoomInfo roomInfo in NetworkData.Instance.RoomList)
                {
                    GameObject matchItem = Instantiate(MatchItemTemplate, parentTransform);
                    matchItem.GetComponent<MatchItemTemplate>().TxtSerial.text = "" + i++;
                    matchItem.GetComponent<MatchItemTemplate>().TxtRoomName.text = roomInfo.Name;
                    matchItem.GetComponent<MatchItemTemplate>().TxtSize.text = roomInfo.MaxPlayers.ToString();
                    matchItem.GetComponent<MatchItemTemplate>().TxtVacancy.text = (roomInfo.MaxPlayers - roomInfo.PlayerCount).ToString();
                }
            }
            else
            {
                XDebug.Log("No Rooms Found", XDebug.Mask.MatchList, XDebug.Color.Red);
            }
        }
    }
}