using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
    {
        public enum GameState
        {
            SinglePlayer,
            Splash,
            MainMenu,
            WaitingScene,
            Game,
            PauseMenu,
            ResultScreen,
        }

        private GameState CurrentGameState = GameState.Splash;

        public void UpdateGameState(GameState gameState)
        {
            XDebug.Log("Updating Game State", XDebug.Mask.GameStateManager, XDebug.Color.Yellow);
            this.CurrentGameState = gameState;
            MyEventManager.Instance.OnGameStateUpdated.Dispatch();
        }

        public GameState GetCurrentGameState()
        {
            return this.CurrentGameState;
        }
    }
}