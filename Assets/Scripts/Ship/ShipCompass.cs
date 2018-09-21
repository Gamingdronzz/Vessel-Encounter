using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCompass : MonoBehaviour
{
    private Transform m_MyTransform;

	// Use this for initialization
	void Start ()
    {
        m_MyTransform = transform;
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        UI.INSTANCE.CompassImage.uvRect = new Rect(m_MyTransform.localEulerAngles.y / 360, 0, 1, 1);
	}
}
