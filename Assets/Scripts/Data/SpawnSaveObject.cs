using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSaveObject : Singleton<SpawnSaveObject>
{
    public List<RaftMove> raftMove = new List<RaftMove>();
    public GameObject raftPrefab = null;

    private GameData gameData = null;

    private void Start()
    {
        gameData = GameManager.Instance.gameData;

        if (gameData.isStart)
        {
            for (int i = 0; i < gameData.raftDatas.Length; i++)
            {
                GameObject raft = Instantiate(raftPrefab, gameData.raftDatas[i].position, Quaternion.identity);

                raft.GetComponentInChildren<InteractableObject>().enabled = true;
                raft.GetComponentInChildren<Collider>().enabled = true;
                raft.GetComponent<WaterFloat>().enabled = true;

                raft.GetComponentInChildren<RaftMove>().SetRaftData(gameData.raftDatas[i]);
            }
        }
    }

    public static RaftData[] GetRaftDatas()
    {
        RaftData[] raftData = new RaftData[Instance.raftMove.Count];

        for (int i = 0; i < Instance.raftMove.Count; i++)
        {
            raftData[i] = Instance.raftMove[i].GetRaftData();
        }

        return raftData;
    }
}
