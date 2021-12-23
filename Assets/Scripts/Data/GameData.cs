using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isStart;
    public bool isLight;
    public int reserchPoint;
    public Vector3 playerPosition;

    public Item[] items;
    public int[] topUICount;
    public int[] reserchLevel;

    public RaftData[] raftDatas;

    public int[] objectId;
    public Vector3[] objectPosition;

    public bool[] sunPowerData;
    public FarmData[] farmData;

    public ResourceData[] resources;
}
