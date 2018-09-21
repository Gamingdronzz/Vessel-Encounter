using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowShip : MonoBehaviour
{
    public float DistanceAway = 4f;
    public float DistanceUp = 4f;

    public Transform Ship;
    public Vector3 Offset;

    private Transform m_MyTransform;
    private Vector3 m_TargetPosition;

    private float m_Smooth = 3f;

	// Use this for initialization
	void Start ()
    {
        m_MyTransform = transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // setting the target position to be the correct offset from the hovercraft
        m_TargetPosition = Ship.position + Vector3.up * DistanceUp - Ship.forward * DistanceAway;

        //m_MyTransform.position = Ship.position + Offset;
        // making a smooth transition between it's current position and the position it wants to be in
        m_MyTransform.position = Vector3.Lerp(transform.position, m_TargetPosition, Time.deltaTime * m_Smooth);

        // make sure the camera is looking the right way!
        transform.LookAt(Ship);
    }
}
