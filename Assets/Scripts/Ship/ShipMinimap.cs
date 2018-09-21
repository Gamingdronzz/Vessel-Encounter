using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMinimap : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Minimap.INSTANCE.ShipTransform = transform;
    }
}
