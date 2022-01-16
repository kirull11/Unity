using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContrl : MonoBehaviour
{
    private bool HasRedKey { get; set; }
    private bool HasBlueKey { get; set; }
    private bool HasGreenKey { get; set; }

    public void GiveKey(DoorType key)
    {
        switch (key)
        {
            case DoorType.Red:
                HasRedKey = true;
                break;
            case DoorType.Blue:
                HasBlueKey = true;
                break;
            case DoorType.Green:
                HasGreenKey = true;
                break;
        }
    }

    public bool HasKey(DoorType door)
    {
        switch (door)
        {
            case DoorType.Red:
                return HasRedKey;
            case DoorType.Blue:
                return HasBlueKey;
            case DoorType.Green:
                return HasGreenKey;
            default:
                return false;
        }
    }
}

public enum DoorType
{
    Red,
    Blue,
    Green
}
