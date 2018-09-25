using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VesselEncounter.Data;

namespace VesselEncounter
{
    public class GameManager : SingletonMonoBehavourPUNCallbacks<GameManager>
    {
        private const string m_GameVersion = "v1";
        private const int MaximumRoomsUnconditionalJoin = 10;

        private void Awake()
        {
            Connect();
        }

        /// <summary>
        /// Connect to the Photon Server
        /// </summary>
        private void Connect()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.GameVersion = m_GameVersion;
                PhotonNetwork.ConnectUsingSettings();
                XDebug.Log("Connected", null, XDebug.Color.Green);
            }
            else
            {
                XDebug.Log("Not Connected", null, XDebug.Color.Red);
            }
        }

        public void CreateOrJoinRoom()
        {
            Hashtable keyValuePairs = new Hashtable();
            keyValuePairs.Add(RoomPropertyKeys.Key_SkillLevel, GameData.Instance.MinimumSkillLevel);
            keyValuePairs.Add(RoomPropertyKeys.Key_MatchWaitTime, GameData.Instance.MatchWaitTime);
            keyValuePairs.Add(RoomPropertyKeys.Key_RoomCreateTime, PhotonNetwork.ServerTimestamp);
            XDebug.Log("Room Create Time if I create room = " + keyValuePairs[RoomPropertyKeys.Key_RoomCreateTime], XDebug.Mask.GameManager, XDebug.Color.Red);
            GameData.Instance.RoomOptions = DefaultRoomOptions(keyValuePairs);
            //keyValuePairs.Add("ServerTimeStamp", PhotonNetwork.Time);
            //keyValuePairs.Add("delayTime", time);
            //PhotonNetwork.JoinOrCreateRoom("Default Room-" + counter, roomOptions, TypedLobby.Default);
            if (PhotonNetwork.IsConnectedAndReady)
            {
                GameData.Instance.PlayerLevel = UnityEngine.Random.Range(1, 100);
                GameData.Instance.UpdateRoomLevel();
                XDebug.Log("Room Count = " + PhotonNetwork.CountOfRooms + "\nInitial Room Level = " + GameData.Instance.MinimumSkillLevel + "\nPlayer Level = " + GameData.Instance.PlayerLevel);
                if (PhotonNetwork.CountOfRooms < MaximumRoomsUnconditionalJoin)
                {
                    XDebug.Log("First Condition");
                    JoinRandomRoom(false);
                }
                else
                {
                    XDebug.Log("Else Condition");
                    JoinRandomRoom(true);
                }
            }
            else
            {
                RuntimeDebug.Instance.AddLog("Not Ready to join yet");
            }
        }

        private void JoinRandomRoom(bool SkillBasedMatchmaking)
        {
            if (!SkillBasedMatchmaking)
                PhotonNetwork.JoinRandomRoom();
            else
            {
                if (GameData.Instance.RoomOptions != null)
                    PhotonNetwork.JoinRandomRoom(GameData.Instance.RoomOptions.CustomRoomProperties, GameData.Instance.MaxPlayers);
                else
                    XDebug.Log("Room Options Not Defined, Define Room Options First", XDebug.Mask.GameManager, XDebug.Color.Red);
            }
        }

        private RoomOptions DefaultRoomOptions(Hashtable keyValuePairs)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = GameData.Instance.MaxPlayers;
            roomOptions.PublishUserId = true;
            roomOptions.CustomRoomPropertiesForLobby = new string[] { RoomPropertyKeys.Key_SkillLevel };
            roomOptions.CustomRoomProperties = keyValuePairs;
            return roomOptions;
        }

        public override void OnConnected()
        {
            base.OnConnected();
            XDebug.Log("On Connected", XDebug.Mask.GameManager, null);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            XDebug.Log("On Create Room Failed - code = " + returnCode + "\nMessage = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            //XDebug.Log("Random Room Failed code = " + returnCode + " message = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
            if (PhotonNetwork.CountOfRooms < 2)
            {
                PhotonNetwork.CreateRoom("", GameData.Instance.RoomOptions, null, null);
            }
            else
            {
                if (returnCode == (int)JoinRoomFailCode.NoMatchFound)
                {
                    //XDebug.Log("Current Room Level = " + GameData.Instance.MinimumSkillLevel.ToString(), XDebug.Mask.GameManager, XDebug.Color.Yellow);
                    if (GameData.Instance.MinimumSkillLevel != GameLevelBrackets.One_Ten)
                    {
                        Hashtable keyValuePairs = new Hashtable();
                        keyValuePairs.Add(RoomPropertyKeys.Key_SkillLevel, --GameData.Instance.MinimumSkillLevel);
                        GameData.Instance.RoomOptions.CustomRoomProperties = keyValuePairs;
                        JoinRandomRoom(true);
                    }
                    else
                        PhotonNetwork.CreateRoom("", GameData.Instance.RoomOptions, null, null);
                }
            }
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            XDebug.Log("On Join Room Failed - code = " + returnCode + "\nMessage = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
            if (returnCode == (int)JoinRoomFailCode.GameFull)
            {
            }
        }

        public override void OnCreatedRoom()
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room != null)
            {
                GameData.Instance.MatchWaitTime = (int)room.CustomProperties[RoomPropertyKeys.Key_MatchWaitTime];
                RuntimeDebug.Instance.AddLog("On Create Room " + room.Name);
                RuntimeDebug.Instance.AddLog("max Players = " + room.MaxPlayers);
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            XDebug.Log("On Disconnected " + cause.ToString(), XDebug.Mask.GameManager, XDebug.Color.Red);
        }

        public override void OnJoinedRoom()
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room == null)
            {
                XDebug.Log("Implement Menu Manager and add functionality to show main menu again on fail to join room", XDebug.Mask.GameManager, XDebug.Color.Red);
                return;
            }
            if (room != null)
            {
                XDebug.Log("On Join Room - " + room.Name + "Skill Level = " + room.CustomProperties[RoomPropertyKeys.Key_SkillLevel], XDebug.Mask.GameManager, null);
                XDebug.Log("Joined Room Creation Time - " + (int)room.CustomProperties[RoomPropertyKeys.Key_RoomCreateTime], XDebug.Mask.GameManager, null);
                NetworkData.Instance.CurrentRoom = room;
                GameData.Instance.MatchWaitTime = (int)room.CustomProperties[RoomPropertyKeys.Key_MatchWaitTime];

                //SceneManager.Instance.LoadScene(SceneManager.Scene.Game, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            XDebug.Log("On Player Enter Room - " + newPlayer.NickName, XDebug.Mask.GameManager, XDebug.Color.Green);
            if (PhotonNetwork.IsMasterClient && NetworkData.Instance.CurrentRoom.MaxPlayers == NetworkData.Instance.CurrentRoom.PlayerCount)
            {
                XDebug.Log("All Players joined ,loading level");
                PhotonNetwork.LoadLevel(SceneManager.Scene.Game.ToString());
            }
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            XDebug.Log("On Player Left Room - " + otherPlayer.NickName + "\nTotal Player Count = " + NetworkData.Instance.CurrentRoom.PlayerCount, XDebug.Mask.GameManager, XDebug.Color.Red);
        }

        public override void OnConnectedToMaster()
        {
            XDebug.Log("On Connected to master", XDebug.Mask.GameManager, null);
            PhotonNetwork.NickName = "Player - " + UnityEngine.Random.Range(0, 9999);
            PhotonNetwork.AutomaticallySyncScene = true;
            MyEventManager.Instance.OnConnectedToMaster.Dispatch();
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            XDebug.Log("Updated room list", XDebug.Mask.GameManager, XDebug.Color.Yellow);
            NetworkData.Instance.UpdateRoomList(roomList);
        }

        public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
            XDebug.Log("On Lobby Statistics Update\nCount = " + lobbyStatistics[0].RoomCount, XDebug.Mask.GameManager, XDebug.Color.Yellow);
        }

        public override void OnLeftLobby()
        {
            throw new System.NotImplementedException();
        }

        public override void OnJoinedLobby()
        {
            XDebug.Log("On Joined Lobby", XDebug.Mask.GameManager, null);
        }

        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            throw new System.NotImplementedException();
        }

        public override void OnLeftRoom()
        {
            throw new System.NotImplementedException();
        }

        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            base.OnRegionListReceived(regionHandler);
            NetworkData.Instance.UpdateRegionList(regionHandler.EnabledRegions);

            //foreach (Region region in regionHandler.EnabledRegions)
            //{
            //    XDebug.Log("Region Code = " + region.Code, XDebug.Mask.GameManager, XDebug.Color.Cyan);
            //    XDebug.Log("Region Ping = " + region.Ping, XDebug.Mask.GameManager, XDebug.Color.Red);
            //}
        }

        public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
            throw new System.NotImplementedException();
        }

        public enum JoinRoomFailCode
        {
            GameFull = 32765,
            NoMatchFound = 32760,
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            XDebug.Log("Room Properties Update", XDebug.Mask.GameManager, XDebug.Color.Yellow);
            if (propertiesThatChanged.ContainsKey(RoomPropertyKeys.Key_RoomCreateTime))
            {
                GameData.Instance.MatchWaitTime = (PhotonNetwork.ServerTimestamp - ((int)(propertiesThatChanged[RoomPropertyKeys.Key_RoomCreateTime]) / 1000));
            }
            else
            {
            }
        }
    }
}