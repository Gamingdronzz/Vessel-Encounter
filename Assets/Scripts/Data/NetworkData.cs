using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter.UI.MainMenu;

namespace VesselEncounter
{
    public class NetworkData : SingletonMonoBehaviour<NetworkData>
    {
        private List<Region> Regions = new List<Region>();
        public Region BestRegion;
        public List<RoomInfo> RoomList { get; internal set; }

        public void UpdateRegionList(List<Region> regions)
        {
            if (regions != null)
            {
                Regions = new List<Region>(regions);
                MyEventManager.Instance.OnRegionListUpdated.Dispatch();
                //OnRegionListUpdated();
            }
            else
            {
                XDebug.Log("Null List Received\nNothing to Update", XDebug.Mask.NetworkData, XDebug.Color.Red);
            }
        }

        private void OnEnable()
        {
            MyEventManager.Instance.OnConnectedToMaster.EventActionVoid += UpdateBestRegion;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnConnectedToMaster.EventActionVoid -= UpdateBestRegion;
        }

        public List<Region> GetRegionList()
        {
            return new List<Region>(Regions);
        }

        public int GetBestRegionIndex()
        {
            return Regions.IndexOf(BestRegion);
        }

        public int GetRegionIndex(Region region)
        {
            return Regions.IndexOf(region);
        }

        private void OnRegionListUpdated()
        {
            //MainMenu.Instance.UpdateRegionList();
        }

        public void UpdateBestRegion()
        {
            Region bestRegion = PhotonNetwork.NetworkingClient.RegionHandler.BestRegion;
            if (bestRegion != null)
            {
                BestRegion = bestRegion;
                MyEventManager.Instance.OnConnectedToBestRegion.Dispatch();
            }
            else
            {
                XDebug.Log("Null Region Received\nUnable to Update Best Region", XDebug.Mask.NetworkData, XDebug.Color.Red);
            }
            //MainMenu.Instance.OnConnectedToBestRegion();
        }

        public void UpdateRoomList(List<RoomInfo> roomList)
        {
            this.RoomList = roomList;
            MyEventManager.Instance.OnRoomListUpdated.Dispatch();
        }
    }
}