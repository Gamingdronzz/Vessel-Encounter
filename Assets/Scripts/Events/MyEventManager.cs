using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class MyEventManager : SingletonMonoBehaviour<MyEventManager>
    {
        public MyEvent OnRegionListUpdated = new MyEvent();
        public MyEvent OnConnectedToBestRegion = new MyEvent();
        public MyEvent OnRoomListUpdated = new MyEvent();
        public MyEvent OnConnectedToMaster = new MyEvent();
        public MyEvent OnPlayerNameChanged = new MyEvent();
        public MyEvent OnGameStateUpdated = new MyEvent();
        public MyEvent OnGameWaitTimeOver = new MyEvent();
        public MyEvent OnLoadingFinished = new MyEvent();
    }
}