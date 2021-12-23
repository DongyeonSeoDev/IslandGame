using UnityEngine;

public class Resource : MonoBehaviour
{
    public int id;

    private void Start()
    {
        SpawnSaveObject.Instance.resource.Add(this);
    }

    private void OnDisable()
    {
        SpawnSaveObject.Instance.resource.Remove(this);
    }
}
