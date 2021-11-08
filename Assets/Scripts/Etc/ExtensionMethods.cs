using UnityEngine;

public static class ExtensionMethods
{
    public static void SetActiveAndBake(this GameObject gameObject, bool active)
    {
        gameObject.SetActive(active);
        NavMeshManager.NavMeshBake();
    }
}
