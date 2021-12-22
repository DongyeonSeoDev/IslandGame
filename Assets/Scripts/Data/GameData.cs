using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool isStart;
    public int reserchPoint;
    public Vector3 playerPosition;

    public Item[] items;
    public int[] topUICount;
    public int[] reserchLevel;

    public RaftData[] raftDatas;
}
