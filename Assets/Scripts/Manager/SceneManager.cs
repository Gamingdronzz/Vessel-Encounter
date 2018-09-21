using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace VesselEncounter
{
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        private Scene? currentScene = null;

        private void Awake()
        {
            XDebug.EnableAllMasks();
        }

        public void LoadScene(Scene scene, LoadSceneMode? loadSceneMode)
        {
            LoadSceneMode sceneMode;
            PlaySceneOutAnim(currentScene);
            if (loadSceneMode == null)
                sceneMode = LoadSceneMode.Single;
            else
                sceneMode = (LoadSceneMode)loadSceneMode;
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.ToString(), sceneMode);
            currentScene = scene;
        }

        private void PlaySceneOutAnim(Scene? currentScene)
        {
            if (currentScene == null)
                return;
            Debug.LogWarning("Implement Scene Out Anim here");
            //TODO
            //Move out current scene here
        }

        public enum Scene
        {
            Splash,
            MainMenu,
            Game
        }
    }
}