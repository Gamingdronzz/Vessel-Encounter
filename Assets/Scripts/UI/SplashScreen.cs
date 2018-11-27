using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VesselEncounter.UI.SplashScreen
{
    public class SplashScreen : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] m_Sprites;

        [SerializeField]
        private Image m_Background;

        private bool m_IsLoadingMainMenu = false;

        [SerializeField]
        private WaitForSeconds ImageSwapDelay = new WaitForSeconds(3f);

        public void Start()
        {
            LoaderSlideshow();
        }

        private void LoaderSlideshow()
        {
            if (m_Sprites != null || m_Sprites.Length != 0)
            {
                StartCoroutine(ImageSlideshow());
            }
        }

        private IEnumerator ImageSlideshow()
        {
            int counter = 0;
            while (!m_IsLoadingMainMenu)
            {
                m_Background.sprite = m_Sprites[counter];
                yield return ImageSwapDelay;
                counter++;
                if (counter == m_Sprites.Length)
                    counter = 0;
            }
        }

        public void LoadMainMenu()
        {
            m_IsLoadingMainMenu = true;
            LocalizationManager.Instance.LoadLocalizedText("English.json");
        }

        public void LoadChangeLanguageMenu()
        {
            SceneManager.Instance.LoadScene(SceneManager.Scene.SelectLanguage, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }
    }
}