using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mount ID Holder is going to give the position about 
/// the mounting of anything to the ship.
/// The ID listed below represents the case for its posiiton mounting
/// Top = 0
/// Bottom = 1
/// Front = 2
/// Back = 3
/// </summary>
public class MountIDHolder : MonoBehaviour
{
    public int MountItemID = 0;

    private void OnTriggerEnter(Collider other)
    {
        ShipMountController.INSTANCE.MountGameObject(MountItemID);
        Destroy(gameObject);
    }
}
