using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class GameData : SingletonMonoBehaviour<GameData>
    {
        public List<RoomInfo> rooms;
        public Room currentRoom;
        public int PlayerLevel = 2;
        public int RoomLevel;
        public RoomOptions roomOptions;
    }
}