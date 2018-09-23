using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    public float FrameCounter = 20;

    private Quaternion m_From;
    private Quaternion m_To;

    private Transform m_CameraGameObject;
    private Transform m_MyTransform;

    private float m_TimeCount = 0.0f;

    private void Awake()
    {
        m_CameraGameObject = GameObject.Find("Main_Camera").transform;
    }

    void Start()
    {
        m_MyTransform = transform;
        m_From = m_MyTransform.localRotation;
    }

    private void LateUpdate()
    {
        m_To = m_CameraGameObject.rotation;
        m_To.x = 0.0f;
        m_To.z = 0.0f;

        m_MyTransform.localRotation = Quaternion.Slerp(m_From, m_To, m_TimeCount);
        m_TimeCount += Time.deltaTime;
    }
}