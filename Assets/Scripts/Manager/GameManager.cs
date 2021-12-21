using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static TopUICount topUICount = new TopUICount(); //UI 카운트 관련 변수

    // 코루틴에서 사용할 변수 미리 정의
    private WaitForSeconds ws = new WaitForSeconds(3f);
    private WaitForSeconds startDelay;

    public event Action gameOverEvent; // 게임 오버시 이벤트 실행

    public float gameStartTime = 3f; // 게임 시작 시간
    public bool isSunPower = false; // 발전기인지 확인
    public bool isBoat = false; // 보트인지 확인
    public bool isLight = false; // 불빛을 사용하고 있는지 확인

    // 현재 상태를 확인하는 함수들
    private PlayerMove playerMove;
    private Coroutine currentCoroutine = null;
    public GameObject buildObject = null;
    public Vector3 currentInteractablePosition;
    public IInteractable currentInteractable;

    public GameData gameData = null;

    protected override void Awake()
    {
        base.Awake();
        playerMove = FindObjectOfType<PlayerMove>();
        gameData = SaveAndLoadManager.Load();
    }

    private void Start()
    {
        if (gameData.isStart)
        {
            for (int i = 0; i < gameData.topUICount.Length; i++)
            {
                topUICount[(TopUI)i] = gameData.topUICount[i];
            }
        }
        else
        {
            topUICount[TopUI.Food] = 10;
            topUICount[TopUI.Water] = 100;
        }

        // 변수 초기화
        startDelay = new WaitForSeconds(gameStartTime);

        gameOverEvent += () =>
        {
            StopCoroutine(currentCoroutine);
        };

        currentCoroutine = StartCoroutine(RemoveFoodAndWater());
    }

    private IEnumerator RemoveFoodAndWater() //물과 음식이 줄어드는 코루틴
    {
        yield return startDelay;

        while (true)
        {
            // 물이 줄어들음
            for (int i = 0; i < 10; i++)
            {
                yield return ws;

                topUICount[TopUI.Water] -= 1;

                if (topUICount[TopUI.Water] < 0)
                {
                    Debug.Log("물 때문에 게임 오버");

                    topUICount[TopUI.Water] = 0;
                    GameOver();
                }
            }

            //음식이 줄어들음
            topUICount[TopUI.Food] -= 1;

            if (topUICount[TopUI.Food] < 0)
            {
                Debug.Log("음식 때문에 게임 오버");

                topUICount[TopUI.Food] = 0;
                GameOver();
            }
        }
    }

    /// <summary>
    /// 플레이어 움직임
    /// </summary>
    /// <param name="playerEvent">플레이어가 목표 지점에 도착하면 이 함수를 실행</param>
    /// <param name="isLock">플레이어가 이동중일때 다른 곳을 클릭해도 이동하지 않음</param>
    /// <param name="targetPosition">목표 위치, null 이라면 currentInteractablePosition을 사용함</param>
    /// <param name="isAgentCheck">네브메쉬 버그를 막기 위해 보트가 이동중일때는 false를 넣어야함</param>
    public void PlayerMove(Action playerEvent, bool isLock = false, Vector3? targetPosition = null, bool isAgentCheck = true)
    {
        if (!playerMove.playerEventLock) // lock이 아니면 실행
        {
            if (isLock) // lock 설정
            {
                playerMove.playerEventLock = true;
                playerEvent += () => playerMove.playerEventLock = false;
            }

            if (targetPosition == null) // 위치 설정
            {
                targetPosition = currentInteractablePosition;
            }

            //PlayerMove로 데이터를 보냄
            playerMove.playerEvent = playerEvent;
            playerMove.isAgentCheck = isAgentCheck;

            //플레이어 이동 실행
            playerMove.Event((Vector3)targetPosition);

            SaveAndLoadManager.Save(new GameData() { isStart = true, playerPosition = playerMove.transform.position, items = InventoryManager.Instance.items, topUICount = topUICount.GetAllTopUICount(), reserchPoint = ResearchManager.Instance.ReserchPoint });
        }
    }

    public void GameOver() // 게임오버시 실행
    {
        gameOverEvent();
    }
}