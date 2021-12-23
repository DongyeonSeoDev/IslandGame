using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSaveObject : Singleton<SpawnSaveObject>
{
    [HideInInspector]  public List<RaftMove> raftMove = new List<RaftMove>();
    [HideInInspector]  public List<InteractableObject> spawnData = new List<InteractableObject>();
    [HideInInspector]  public List<InteractableObject> farms = new List<InteractableObject>();
    [HideInInspector]  public List<InteractableObject> sunPowers = new List<InteractableObject>();
    [HideInInspector]  public List<Resource> resource = new List<Resource>();

    public GameObject raftPrefab = null;

    public GameObject[] spawnObjects;
    public GameObject[] spawnResources;
    public Vector3[] resourceRotationDatas;

    public GameObject resourceObject = null;

    private GameData gameData = null;

    private int farmId = 0;
    private int sunPowerId = 0;

    private void Start()
    {
        gameData = GameManager.Instance.gameData;

        SpawnObject();
        SpawnResource();
    }

    private void SpawnObject()
    {
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

            for (int i = 0; i < gameData.objectId.Length; i++)
            {
                Quaternion rotateData = Quaternion.identity;

                if (gameData.objectId[i] > 0)
                {
                    rotateData = Quaternion.Euler(new Vector3(-90f, 90f, 0f));
                }

                if (gameData.objectId[i] == 4)
                {
                    rotateData = Quaternion.Euler(new Vector3(-90f, 0f, 0f));

                    GameObject go = Instantiate(spawnObjects[gameData.objectId[i]], gameData.objectPosition[i], rotateData);

                    go.GetComponent<Collider>().enabled = true;

                    InteractableObject interactableObject = go.GetComponent<InteractableObject>();
                    interactableObject.enabled = true;

                    FarmData farmData = gameData.farmData[farmId];

                    interactableObject.waterCount = farmData.waterCount;
                    interactableObject.isFarmFieldSeed = farmData.isFarmFieldSeed;
                    interactableObject.isFarmFieldWater = farmData.isFarmFieldWater;
                    interactableObject.isFarmFieldComplete = farmData.isFarmFieldComplete;
                    interactableObject.isWaterTime = farmData.isWaterTime;

                    if (interactableObject.isFarmFieldSeed)
                    {
                        interactableObject.SeedChange();
                    }

                    if (interactableObject.isFarmFieldWater)
                    {
                        interactableObject.WaterMaterialChange();
                    }

                    if (interactableObject.isFarmFieldComplete)
                    {
                        interactableObject.CompleteChange();
                    }

                    if (interactableObject.isWaterTime)
                    {
                        interactableObject.StartWaterTime();
                    }

                    farmId++;
                }
                else if (gameData.objectId[i] == 5)
                {
                    rotateData = Quaternion.Euler(new Vector3(-50f, 180f, -90f));

                    GameObject go = Instantiate(spawnObjects[gameData.objectId[i]], gameData.objectPosition[i], rotateData);

                    go.GetComponent<Collider>().enabled = true;

                    InteractableObject interactableObject = go.GetComponent<InteractableObject>();

                    interactableObject.enabled = true;
                    interactableObject.isUseSunPower = gameData.sunPowerData[sunPowerId];
                    sunPowerId++;
                }
                else
                {
                    GameObject go = Instantiate(spawnObjects[gameData.objectId[i]], gameData.objectPosition[i], rotateData);

                    go.GetComponent<InteractableObject>().enabled = true;
                    go.GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    private void SpawnResource()
    {
        if (gameData.isStart)
        {
            Destroy(resourceObject);

            for (int i = 0; i < gameData.resources.Length; i++)
            {
                GameObject resource = Instantiate(spawnResources[gameData.resources[i].id], gameData.resources[i].position, Quaternion.Euler(resourceRotationDatas[gameData.resources[i].id]));

                if (gameData.resources[i].id == 3)
                {
                    InteractableObject interactable = resource.GetComponent<InteractableObject>();

                    interactable.isStone = gameData.resources[i].isActive;

                    if (!interactable.isStone)
                    {
                        interactable.StoneTime();
                    }
                }
                else if (gameData.resources[i].id == 4 || gameData.resources[i].id == 5)
                {
                    InteractableObject interactable = resource.GetComponent<InteractableObject>();

                    interactable.isTree = gameData.resources[i].isActive;

                    if (!interactable.isTree)
                    {
                        interactable.WoodTime();
                    }
                }
            }
        }
    }

    private bool IsActiveValue(int i, int id)
    {
        if (id == 3)
        {
            return Instance.resource[i].GetComponent<InteractableObject>().isStone;
        }
        else if (id == 4 || id == 5)
        {
            return Instance.resource[i].GetComponent<InteractableObject>().isTree;
        }

        return false;
    }

    public static ResourceData[] GetSpawnResourceData()
    {
        ResourceData[] data = new ResourceData[Instance.resource.Count];

        for (int i = 0; i < Instance.resource.Count; i++)
        {
            data[i] = new ResourceData()
            {
                id = Instance.resource[i].id,
                position = Instance.resource[i].transform.position,
                isActive = Instance.IsActiveValue(i, Instance.resource[i].id)
            };
        }

        return data;
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

    public static int[] GetSpawnIds()
    {
        int[] datas = new int[Instance.spawnData.Count];

        for (int i = 0; i < Instance.spawnData.Count; i++)
        {
            datas[i] = Instance.spawnData[i].id;
        }

        return datas;
    }

    public static Vector3[] GetSpawnPosition()
    {
        Vector3[] datas = new Vector3[Instance.spawnData.Count];

        for (int i = 0; i < Instance.spawnData.Count; i++)
        {
            datas[i] = Instance.spawnData[i].transform.position;
        }

        return datas;
    }

    public static bool[] GetSunPower()
    {
        bool[] sunPowers = new bool[Instance.sunPowers.Count];

        for (int i = 0; i < Instance.sunPowers.Count; i++)
        {
            sunPowers[i] = Instance.sunPowers[i].isUseSunPower;
        }

        return sunPowers;
    }

    public static FarmData[] GetFarmData()
    {
        FarmData[] farmData = new FarmData[Instance.farms.Count];

        for (int i = 0; i < Instance.sunPowers.Count; i++)
        {
            farmData[i] = new FarmData()
            {
                waterCount = Instance.farms[i].waterCount,
                isFarmFieldSeed = Instance.farms[i].isFarmFieldSeed,
                isFarmFieldWater = Instance.farms[i].isFarmFieldWater,
                isFarmFieldComplete = Instance.farms[i].isFarmFieldComplete,
                isWaterTime = Instance.farms[i].isWaterTime
            };
        }

        return farmData;
    }
}
