using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhotonView))]
public class PlayerPhotonManagement : MonoBehaviour
{
    [SerializeField]
    private PhotonView m_PhotonView;

    [SerializeField]
    private GameObject m_CameraController;

    [SerializeField]
    private ShipController m_ShipController;

    [SerializeField]
    private FloatObject m_floatObject;

    private void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        if (m_PhotonView != null && !m_PhotonView.IsMine)
        {
            if (m_CameraController != null && m_ShipController != null)
            {
                m_CameraController.SetActive(false);
            }
            if (m_ShipController != null)
            {
                m_ShipController.enabled = false;
            }
            if (m_floatObject != null)
            {
                m_floatObject.enabled = false;
            }
        }
    }
}