using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public float frameCounter = 20;

    private Quaternion m_From;
    private Quaternion m_To;

    private Transform m_CameraGameObject;

    private float timeCount = 0.0f;

    private void Awake()
    {
        m_CameraGameObject = GameObject.Find("Main_Camera").transform;
    }

    void Start()
    {
        m_From = transform.localRotation;
    }

    private void LateUpdate()
    {
        m_To = m_CameraGameObject.rotation;
        m_To.x = 0.0f;
        m_To.z = 0.0f;

        transform.localRotation = Quaternion.Slerp(m_From, m_To, timeCount);
        timeCount += Time.deltaTime;
    }
}