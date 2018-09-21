using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

namespace VesselEncounter
{
    public class GameManager : SingletonMonoBehavourPUNCallbacks<GameManager>
    {
        private const string m_GameVersion = "v1";
        private int counter = 1;

        private void Awake()
        {
            Connect();
        }

        // Use this for initialization
        private void Start()
        {
        }

        // Update is called once per frame
        private void Update()
        {
        }

        private void CustomRoom()
        {
        }

        public void DefaultRoom()
        {
            XDebug.Log("Default Room", null, XDebug.Color.Green);
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
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
                RuntimeDebug.Instance.AddLog("Connected");
            }
            else
            {
                XDebug.Log("Not Connected", null, XDebug.Color.Red);
                RuntimeDebug.Instance.AddLog("Not Connected");
            }
        }

        public void CreateOrJoinRoom(int time, byte maxPlayers)
        {
            RoomOptions roomOptions = new RoomOptions();
            roomOptions.IsOpen = true;
            roomOptions.IsVisible = true;
            roomOptions.MaxPlayers = maxPlayers;
            roomOptions.PublishUserId = true;
            Hashtable keyValuePairs = new Hashtable();
            //keyValuePairs.Add("ServerTimeStamp", PhotonNetwork.Time);
            //keyValuePairs.Add("delayTime", time);

            //PhotonNetwork.JoinOrCreateRoom("Default Room-" + counter, roomOptions, TypedLobby.Default);

            if (PhotonNetwork.CountOfRooms < 20)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                int level = 1;
                if (GameData.Instance.PlayerLevel >= 2)
                {
                    level = 2;
                }
                else if (GameData.Instance.PlayerLevel >= 1)
                {
                    level = 1;
                }
                GameData.Instance.RoomLevel = level;
                keyValuePairs.Add("minimum-skill-level", level);

                roomOptions.CustomRoomPropertiesForLobby = new string[] { "minimum-skill-level" };
                roomOptions.CustomRoomProperties = keyValuePairs;
                GameData.Instance.roomOptions = roomOptions;
                PhotonNetwork.JoinRandomRoom(keyValuePairs, maxPlayers);
            }
        }

        public override void OnConnected()
        {
            base.OnConnected();
            XDebug.Log("On Connected", XDebug.Mask.GameManager, null);
            RuntimeDebug.Instance.AddLog("On Connected");
        }

        //public override void OnLeftRoom()
        //{
        //    base.OnLeftRoom();
        //    XDebug.Log("On Left Room", XDebug.Mask.GameManager, null);
        //}

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            XDebug.Log("On Create Room Failed - code = " + returnCode + "\nMessage = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
            RuntimeDebug.Instance.AddLog("On Create Room Failed - code = " + returnCode + "\nMessage = " + message);
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            XDebug.Log("On Join Room Failed - code = " + returnCode + "\nMessage = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
            RuntimeDebug.Instance.AddLog("On Join Room Failed - code = " + returnCode + "\nMessage = " + message);
            if (returnCode == (int)JoinRoomFailCode.GameFull)
            {
            }
        }

        public override void OnCreatedRoom()
        {
            Room room = PhotonNetwork.CurrentRoom;
            if (room != null)
                XDebug.Log("On Create Room " + room.Name + "\nmax Players = " + room.MaxPlayers, XDebug.Mask.GameManager, null);
        }

        //public override void OnLeftLobby()
        //{
        //    base.OnLeftLobby();
        //    XDebug.Log("On Left Lobby", XDebug.Mask.GameManager, null);
        //}

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
                XDebug.Log("On Join Room " + room.Name + "\nmax Players = " + room.MaxPlayers + "Custom = " + room.CustomProperties["minimum-skill-level"], XDebug.Mask.GameManager, null);
                RuntimeDebug.Instance.AddLog("On Join Room " + room.Name + "\nmax Players = " + room.MaxPlayers + "Custom = " + room.CustomProperties["minimum-skill-level"]);
                GameData.Instance.currentRoom = room;

                SceneManager.Instance.LoadScene(SceneManager.Scene.Game, UnityEngine.SceneManagement.LoadSceneMode.Single);
            }
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            XDebug.Log("On Player Enter Room - " + newPlayer.NickName, XDebug.Mask.GameManager, XDebug.Color.Green);
            RuntimeDebug.Instance.AddLog("On Player Enter Room - " + newPlayer.NickName);
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            XDebug.Log("On Player Left Room - " + otherPlayer.NickName, XDebug.Mask.GameManager, XDebug.Color.Red);
        }

        public override void OnConnectedToMaster()
        {
            XDebug.Log("On Connected to master", XDebug.Mask.GameManager, null);
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            XDebug.Log("Updated room list", XDebug.Mask.GameManager, XDebug.Color.Yellow);
            GameData.Instance.rooms = roomList;
        }

        public void UpdateRoomList()
        {
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

        //void IInRoomCallbacks.OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        //{
        //    throw new System.NotImplementedException();
        //}

        //void IInRoomCallbacks.OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        //{
        //    throw new System.NotImplementedException();
        //}

        //void IInRoomCallbacks.OnMasterClientSwitched(Player newMasterClient)
        //{
        //    throw new System.NotImplementedException();
        //}

        public override void OnFriendListUpdate(List<FriendInfo> friendList)
        {
            throw new System.NotImplementedException();
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            XDebug.Log("Random Room Failed code = " + returnCode + " message = " + message, XDebug.Mask.GameManager, XDebug.Color.Red);
            if (returnCode == (int)JoinRoomFailCode.NoMatchFound)
            {
                if (PhotonNetwork.CountOfRooms > 100)
                    PhotonNetwork.CreateRoom("", GameData.Instance.roomOptions, null, null);
            }
        }

        public override void OnLeftRoom()
        {
            throw new System.NotImplementedException();
        }

        public override void OnRegionListReceived(RegionHandler regionHandler)
        {
            base.OnRegionListReceived(regionHandler);
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
    }
}