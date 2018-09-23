using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VesselEncounter;

public class InputManager : SingletonMonoBehaviour<InputManager>
{
    [SerializeField]
    private GameObject ClickBlocker;

    private void Awake()
    {
        if (ClickBlocker != null)
            ClickBlocker.SetActive(false);
    }

    public void ActivateInput(bool activate)
    {
        if (ClickBlocker != null)
            ClickBlocker.SetActive(!activate);
        else
        {
            ClickBlocker = GameObject.Find("ClickBlocker");
            ClickBlocker.SetActive(!activate);
        }
    }
}