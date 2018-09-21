using UnityEngine;

[CreateAssetMenu(fileName ="Create Mountable Item", menuName ="Mount Item")]
public class MountScriptableObject : ScriptableObject
{
    public string MountName;
    public int MountID;
    public GameObject MountGameObject;
    public MountPosition MountPosition;
    public MountPriority MountPriority;
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