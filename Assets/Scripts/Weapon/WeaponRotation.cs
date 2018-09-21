using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    GameObject CameraGo;

	// Use this for initialization
	void Start ()
    {
        CameraGo = GameObject.Find("Main Camera");
	}

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.ScreenToViewportPoint(Input.mousePosition));
	}
}