using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using VesselEncounter.Data;

namespace VesselEncounter {
    public class WaitSceneController : MonoBehaviour
    {

        private WaitForSeconds seconds = new WaitForSeconds(1f);
        public TextMeshProUGUI CountDown;

        // Use this for initialization
        void Start()
        {
            int countdownValue = GameData.Instance.MatchWaitTime;
            XDebug.Log("New Wait Time = " + countdownValue);
            if (CountDown != null)
                StartCoroutine(StartCountdown(countdownValue));
        }

        // Update is called once per frame
        void Update()
        {

        }

        private IEnumerator StartCountdown(int value)
        {
            for (int i = value; i > 0; i--)
            {
                CountDown.text = i.ToString();
                yield return seconds;
            }
            MyEventManager.Instance.OnGamePlayConditionsMet.Dispatch();
        }
    }
}
