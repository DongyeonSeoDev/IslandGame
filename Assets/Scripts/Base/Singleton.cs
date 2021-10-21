using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance is null)
            {
                Debug.Log(typeof(T) + "의 instance가 null 입니다.");
                return null;
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (!(instance is null))
        {
            Debug.LogWarning(typeof(T) + "의 instance가 이미 있습니다.");
            Destroy(this);

            return;
        }

        instance = GetComponent<T>();
    }
}