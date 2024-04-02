using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    [Header("Player Data")]
    // saved player transform
    public float x;
    public float y;
    public float z;

    [Header("time Data")]
    public float timeValue;

    public string playerName;
}
