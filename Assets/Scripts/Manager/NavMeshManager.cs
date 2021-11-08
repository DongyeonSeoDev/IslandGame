using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshManager : Singleton<NavMeshManager>
{
    private NavMeshSurface navMeshSurface = null;

    protected override void Awake()
    {
        base.Awake();
        navMeshSurface = GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        Bake();
    }

    private void Bake()
    {
        navMeshSurface.RemoveData();
        navMeshSurface.BuildNavMesh();
    }

    public static void NavMeshBake()
    {
        Instance.Bake();
    }
}
