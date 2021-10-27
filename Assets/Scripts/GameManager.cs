using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static TopUICount topUICount = new TopUICount();

    public float gameStartTime = 3f;

    public IInteractable currentInteractable;
    public Vector3 currentInteractablePosition;
    public GameObject buildObject = null;

    private WaitForSeconds ws = new WaitForSeconds(3f);
    private WaitForSeconds ws2 = new WaitForSeconds(0.1f);
    private WaitForSeconds startDelay;

    public event Action gameOverEvent;

    private Coroutine currentCoroutine = null;

    public bool isSpeedUp = false;

    private PlayerMove playerMove;

    protected override void Awake()
    {
        base.Awake();
        playerMove = FindObjectOfType<PlayerMove>();
    }

    private void Start()
    {
        startDelay = new WaitForSeconds(gameStartTime);

        topUICount[TopUI.food] = 10;
        topUICount[TopUI.water] = 100;

        gameOverEvent += () =>
        {
            StopCoroutine(currentCoroutine);
        };

        currentCoroutine = StartCoroutine(RemoveFoodAndWater());
    }

    private IEnumerator RemoveFoodAndWater()
    {
        yield return startDelay;

        while (true)
        {
            for (int i = 0; i < 10; i++)
            {
                yield return isSpeedUp ? ws2 : ws;

                topUICount[TopUI.water] -= 1;

                if (topUICount[TopUI.water] < 0)
                {
                    Debug.Log("물 때문에 게임 오버");

                    topUICount[TopUI.water] = 0;
                    gameOverEvent();
                }

                if (topUICount[TopUI.water] < 21)
                {
                    UIManager.Instance.ChangeWarming(TopUI.water, true);
                }
            }

            topUICount[TopUI.food] -= 1;

            if (topUICount[TopUI.food] < 0)
            {
                Debug.Log("음식 때문에 게임 오버");

                topUICount[TopUI.food] = 0;
                gameOverEvent();
            }

            if (topUICount[TopUI.food] < 4)
            {
                UIManager.Instance.ChangeWarming(TopUI.food, true);
            }
        }
    }

    public void PlayerMove(Action playerEvent, bool isLock = false)
    {
        if (!playerMove.playerEventLock)
        {
            if (isLock)
            {
                playerMove.playerEventLock = true;
                playerEvent += () => playerMove.playerEventLock = false;
            }

            playerMove.TargetPosition = currentInteractablePosition;
            playerMove.playerEvent = playerEvent;
            playerMove.Event();
        }
    }
}