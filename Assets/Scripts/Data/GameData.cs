using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter.Data

{
    public class GameData : SingletonMonoBehaviour<GameData>
    {
        public List<RoomInfo> rooms;
        public Room CurrentRoom;
        public byte MaxPlayers;
        public int PlayerLevel = 26;
        public GameLevelBrackets RoomLevel;
        public RoomOptions roomOptions;

        public int MatchWaitTime { get; internal set; }

        public enum Brackets
        {
        }

        public void UpdateRoomLevel()
        {
            if (PlayerLevel <= 10)
                RoomLevel = GameLevelBrackets.Ten;
            else if (PlayerLevel <= 25)
                RoomLevel = GameLevelBrackets.TwentyFive;
            else if (PlayerLevel <= 50)
                RoomLevel = GameLevelBrackets.Fifty;
            else if (PlayerLevel <= 100)
                RoomLevel = GameLevelBrackets.Hundred;
            else if (PlayerLevel <= 250)
                RoomLevel = GameLevelBrackets.TwoFifty;
            else
                RoomLevel = GameLevelBrackets.FiveHundred;
        }
    }
}