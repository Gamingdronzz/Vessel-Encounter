using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateContainer : MonoBehaviour
{
    public MountScriptableObject[] m_WeaponScriptableObject;
    public int RandomAmmoAmount;

    // Use this for initialization
    private void Start()
    {
        int randomWeaponPick = Random.Range(0, m_WeaponScriptableObject.Length - 1);

        switch (randomWeaponPick)
        {
            case 0:
                break;

            case 1:
                break;

            case 2:
                break;

            case 3:
                break;
        }
    }
}