using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class MyEventManager : SingletonMonoBehaviour<MyEventManager>
    {
        public MyEvent OnRegionListReceived = new MyEvent();
        public MyEvent OnConnectedToBestRegion = new MyEvent();
        public MyEvent OnRoomListUpdated = new MyEvent();
    }
}