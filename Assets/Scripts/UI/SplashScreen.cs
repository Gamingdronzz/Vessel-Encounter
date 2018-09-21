using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter.UI.SplashScreen
{
    public class SplashScreen : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.Instance.LoadScene(SceneManager.Scene.MainMenu, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}