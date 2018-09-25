using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter.Data

{
    public class GameData : SingletonMonoBehaviour<GameData>
    {
        public byte MaxPlayers;
        public int PlayerLevel = 26;
        public GameLevelBrackets MinimumSkillLevel;
        public RoomOptions RoomOptions;

        public int MatchWaitTime { get; internal set; }

        public void ResetGameData()
        {
            UpdateRoomLevel();
            RoomOptions = null;
        }

        public void UpdateRoomLevel()
        {
            if (PlayerLevel <= 10)
                MinimumSkillLevel = GameLevelBrackets.One_Ten;
            else if (PlayerLevel <= 25)
                MinimumSkillLevel = GameLevelBrackets.Eleven_TwentyFive;
            else if (PlayerLevel <= 50)
                MinimumSkillLevel = GameLevelBrackets.TwentySix_Fifty;
            else if (PlayerLevel <= 100)
                MinimumSkillLevel = GameLevelBrackets.FiftyOne_Hundred;
            else if (PlayerLevel <= 250)
                MinimumSkillLevel = GameLevelBrackets.HundredOne_TwoFifty;
            else
                MinimumSkillLevel = GameLevelBrackets.TwoFiftyOne_FiveHundred;
        }
    }
}