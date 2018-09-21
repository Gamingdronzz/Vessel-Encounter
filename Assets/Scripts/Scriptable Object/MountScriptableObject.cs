using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="Mountable Item", menuName ="Create Mount Item")]
public class MountScriptableObject : ScriptableObject
{
    [Header("Mandatory")]
    public string MountName;
    public int MountID;
    public ItemType ItemType;
    public RawImage ItemImage;
    public GameObject MountGO;
    public GameObject UnMountGO;
    public int Quantity;
    public bool IsStackable;
    public int InventorySpace;

    [Header("Weapon Parameters")]
    public float FireRate;
    public float ReloadTime;
    public AmmoType AmmoType;
    public int MagazineClipLimit;

    [Header("Mount Position Optional")]
    public MountPosition MountPosition;
    public MountPriority MountPriority;
}

public enum ItemType
{
    Item = 1,
    Weapon = 2,
    Ammo = 3
}

public enum AmmoType
{
    Bullet,
    Rocket
}

public enum MountPriority
{
    Level_1 = 1,
    Level_2 = 2,
    Level_3 = 3
}

public enum MountPosition
{
    Top,
    Bottom,
    Front,
    Back
}