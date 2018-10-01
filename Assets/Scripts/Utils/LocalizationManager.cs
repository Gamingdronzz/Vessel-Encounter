using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

namespace VesselEncounter
{
    public class LocalizationManager : SingletonMonoBehaviourPUN<LocalizationManager>
    {
        private Dictionary<string, string> localizedText;
        private string missingTextString = "Localized text not found";

        private void OnEnable()
        {
            MyEventManager.Instance.OnLanguageChanged.EventActionVoid += OnLanguageChanged;
        }

        private void OnDisable()
        {
            MyEventManager.Instance.OnLanguageChanged.EventActionVoid -= OnLanguageChanged;
        }

        private void OnLanguageChanged()
        {
            SceneManager.Instance.LoadScene(SceneManager.Scene.MainMenu, LoadSceneMode.Single);
        }

        public void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }

                Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");

            }
            else
            {
                Debug.LogError("Cannot find file!");
            }

            MyEventManager.Instance.OnLanguageChanged.Dispatch();
        }

        public string GetLocalizedValue(string key)
        {
            string result = missingTextString;
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }

            return result;

        }
    }
}