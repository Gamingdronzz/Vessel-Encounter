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
    private GameObject m_CameraController, m_PlayerController;

    private void Start()
    {
        m_PhotonView = GetComponent<PhotonView>();
        if (m_PhotonView != null && !m_PhotonView.IsMine)
        {
            if (m_CameraController != null && m_PlayerController != null)
            {
                m_CameraController.SetActive(false);
                m_PlayerController.SetActive(false);
            }
        }
    }
}