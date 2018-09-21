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

        public void UpdateRegionList(List<Region> regions)
        {
            if (regions != null)
            {
                Regions = new List<Region>(regions);
                OnRegionListUpdated();
            }
            else
            {
                XDebug.Log("Null List Received\nNothing to Update", XDebug.Mask.NetworkData, XDebug.Color.Red);
            }
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
            MainMenu.Instance.UpdateRegionList(Regions);
        }

        public void OnConnectedToBestRegion()
        {
            MainMenu.Instance.OnConnectedToBestRegion();
        }
    }
}