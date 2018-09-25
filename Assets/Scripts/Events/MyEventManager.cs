﻿using System.Collections;
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
    }
}