using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static TopUICount topUICount = new TopUICount();

    public float gameStartTime = 3f;

    public IInteractable currentInteractable;
    public GameObject buildObject = null;

    private WaitForSeconds ws = new WaitForSeconds(1f);

    private void Start()
    {
        topUICount[TopUI.food] = 10;
        //topUICount[TopUI.water] = 100;

        //StartCoroutine()
    }

    //private IEnumerator RemoveFoodAndWater()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {

    //    }
    //}
}
