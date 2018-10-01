using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VesselEncounter;

public class MountItemManager : SingletonMonoBehaviour<MountItemManager>
{
    //public static MountItemManager INSTANCE;

    [Tooltip("It's important to arrange the items according to the ID's assigned to the scriptable objects. Otherwise" +
        "the entire scriptable objects will spawn wrongly.")]
    public MountScriptableObject[] MountScriptableObject;

    // Use this for initialization
    private void Start()
    {
        //if (INSTANCE == null)
        //{
        //    INSTANCE = this;
        //}
        //else if (INSTANCE != this)
        //{
        //    Destroy(this);
        //}
    }
}