using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public float gameStartTime = 3f;

    public IInteractable currentInteractable;
    public GameObject buildObject = null;
}
