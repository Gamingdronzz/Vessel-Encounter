using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipMountController : MonoBehaviour
{
    public static ShipMountController INSTANCE;

    [Header("Position for mounting the Gameobjects")]
    public GameObject TopPosition;
    public GameObject BottoPosition;
    public GameObject FrontPosition;
    public GameObject BackPosition;

    private bool m_IsTopMounted = false;
    private bool m_IsBottomMounted = false;
    private bool m_IsFrontMounted = false;
    private bool m_IsBackMounted = false;

    private List<CurrentWeaponDataContainer> m_CurrentWeaponAmmoData = new List<CurrentWeaponDataContainer>();

    private MountItemDataContainer[] m_MountedItemList = new MountItemDataContainer[4];

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = this;
        }
        else if(INSTANCE != this)
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        InitializeData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// The ID listed below represents the case for its posiiton mounting
    /// Top = 0
    /// Bottom = 1
    /// Front = 2
    /// Back = 3
    /// </summary>
    public void MountGameObject(int mountID)
    {
        switch (MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPosition.ToString())
        {
            case "Top":
                if (m_IsTopMounted)
                {
                    if (m_MountedItemList[TagManager.TOP_POSITION_TAG].MountedItemPriority < (int)MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPriority)
                    {
                        UnMountItemOnShip(TagManager.TOP_POSITION_TAG);
                        MountItemOnShip(TagManager.TOP_POSITION_TAG, mountID, TopPosition.transform);
                    }
                }
                else
                {
                    MountItemOnShip(TagManager.TOP_POSITION_TAG, mountID, TopPosition.transform);
                    m_IsTopMounted = true;
                }
                break;

            case "Bottom":
                if (m_IsBottomMounted)
                {
                    if (m_MountedItemList[TagManager.BOTTOM_POSITION_TAG].MountedItemPriority < (int)MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPriority)
                    {
                        UnMountItemOnShip(TagManager.BOTTOM_POSITION_TAG);
                        MountItemOnShip(TagManager.BOTTOM_POSITION_TAG, mountID, BottoPosition.transform);
                    }
                }
                else
                {
                    MountItemOnShip(TagManager.BOTTOM_POSITION_TAG, mountID, BottoPosition.transform);
                    m_IsBottomMounted = true;
                }
                break;

            case "Front":
                if (m_IsFrontMounted)
                {
                    if (m_MountedItemList[TagManager.FRONT_POSITION_TAG].MountedItemPriority < (int)MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPriority)
                    {
                        UnMountItemOnShip(TagManager.FRONT_POSITION_TAG);
                        MountItemOnShip(TagManager.FRONT_POSITION_TAG, mountID, FrontPosition.transform);
                    }
                }
                else
                {
                    MountItemOnShip(TagManager.FRONT_POSITION_TAG, mountID, FrontPosition.transform);
                    m_IsFrontMounted = true;
                }
                break;

            case "Back":
                if (m_IsBackMounted)
                {
                    if (m_MountedItemList[TagManager.BACK_POSITION_TAG].MountedItemPriority < (int)MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPriority)
                    {
                        UnMountItemOnShip(TagManager.BACK_POSITION_TAG);
                        MountItemOnShip(TagManager.BACK_POSITION_TAG, mountID, BackPosition.transform);
                    }
                }
                else
                {
                    MountItemOnShip(TagManager.BACK_POSITION_TAG, mountID, BackPosition.transform);
                    m_IsBackMounted = true;
                }
                break;
        }
    }

    //Instantiates and stores it in Mounted Item List
    private void MountItemOnShip(int index, int mountID, Transform mountPosition)
    {
        GameObject go = Instantiate(MountItemManager.INSTANCE.MountScriptableObject[mountID].MountGameObject,
            mountPosition.position,
            mountPosition.rotation,//MountItemManager.INSTANCE.MountScriptableObject[mountID].MountGameObject.transform.rotation,
            transform);

        MountItemDataContainer mountItemDataContainer = new MountItemDataContainer
        {
            MountedItem = go,
            MountedItemPriority = (int)MountItemManager.INSTANCE.MountScriptableObject[mountID].MountPriority
        };

        m_MountedItemList[index] = mountItemDataContainer;
    }

    //Destroy the index object from the array
    private void UnMountItemOnShip(int index)
    {
        Destroy(m_MountedItemList[index].MountedItem.gameObject);
    }

    private void InitializeData()
    {
        for (int i = 0; i < m_CurrentWeaponAmmoData.Count; i++)
        {
            CurrentWeaponDataContainer currentWeaponDataContainer = new CurrentWeaponDataContainer
            {
                CurrentID = i,
                CurrentAmmo = 0,
                CurrentPriority = 0
            };

            m_CurrentWeaponAmmoData.Add(currentWeaponDataContainer);
        }

        if (TopPosition == null || FrontPosition == null ||
            BackPosition == null || BottoPosition == null)
        {
            Debug.Log("<Color='Red'>GameObject is not attached to the script</color>");
        }
    }
}