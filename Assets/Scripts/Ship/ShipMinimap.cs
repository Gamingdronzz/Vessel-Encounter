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
            Minimap.Instance.ShipTransform = transform;
        }
    }
}