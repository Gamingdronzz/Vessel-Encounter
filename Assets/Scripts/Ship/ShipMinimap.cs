using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class ShipMinimap : MonoBehaviour
    {
        // Use this for initialization
        private void Start()
        {
            if (GameStateManager.Instance.GetCurrentGameState() == GameStateManager.GameState.SinglePlayer)
                Minimap.Instance.ShipTransform = transform;
        }
    }
}