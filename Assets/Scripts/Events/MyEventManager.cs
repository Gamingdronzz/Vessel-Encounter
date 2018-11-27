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
        public MyEvent<string> OnPlayerNameChanged = new MyEvent<string>();
        public MyEvent OnGameStateUpdated = new MyEvent();
        public MyEvent OnGamePlayConditionsMet = new MyEvent();
        public MyEvent OnCreatedOrJoinedRoom = new MyEvent();
        public MyEvent OnPlayerJoined = new MyEvent();
        public MyEvent OnPlayerLeft = new MyEvent();
        public MyEvent OnLanguageChanged = new MyEvent();
    }
}