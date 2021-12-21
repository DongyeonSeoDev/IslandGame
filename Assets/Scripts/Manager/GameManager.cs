using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static TopUICount topUICount = new TopUICount(); //UI ī��Ʈ ���� ����

    // �ڷ�ƾ���� ����� ���� �̸� ����
    private WaitForSeconds ws = new WaitForSeconds(3f);
    private WaitForSeconds startDelay;

    public event Action gameOverEvent; // ���� ������ �̺�Ʈ ����

    public float gameStartTime = 3f; // ���� ���� �ð�
    public bool isSunPower = false; // ���������� Ȯ��
    public bool isBoat = false; // ��Ʈ���� Ȯ��
    public bool isLight = false; // �Һ��� ����ϰ� �ִ��� Ȯ��

    // ���� ���¸� Ȯ���ϴ� �Լ���
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

        // ���� �ʱ�ȭ
        startDelay = new WaitForSeconds(gameStartTime);

        gameOverEvent += () =>
        {
            StopCoroutine(currentCoroutine);
        };

        currentCoroutine = StartCoroutine(RemoveFoodAndWater());
    }

    private IEnumerator RemoveFoodAndWater() //���� ������ �پ��� �ڷ�ƾ
    {
        yield return startDelay;

        while (true)
        {
            // ���� �پ����
            for (int i = 0; i < 10; i++)
            {
                yield return ws;

                topUICount[TopUI.Water] -= 1;

                if (topUICount[TopUI.Water] < 0)
                {
                    Debug.Log("�� ������ ���� ����");

                    topUICount[TopUI.Water] = 0;
                    GameOver();
                }
            }

            //������ �پ����
            topUICount[TopUI.Food] -= 1;

            if (topUICount[TopUI.Food] < 0)
            {
                Debug.Log("���� ������ ���� ����");

                topUICount[TopUI.Food] = 0;
                GameOver();
            }
        }
    }

    /// <summary>
    /// �÷��̾� ������
    /// </summary>
    /// <param name="playerEvent">�÷��̾ ��ǥ ������ �����ϸ� �� �Լ��� ����</param>
    /// <param name="isLock">�÷��̾ �̵����϶� �ٸ� ���� Ŭ���ص� �̵����� ����</param>
    /// <param name="targetPosition">��ǥ ��ġ, null �̶�� currentInteractablePosition�� �����</param>
    /// <param name="isAgentCheck">�׺�޽� ���׸� ���� ���� ��Ʈ�� �̵����϶��� false�� �־����</param>
    public void PlayerMove(Action playerEvent, bool isLock = false, Vector3? targetPosition = null, bool isAgentCheck = true)
    {
        if (!playerMove.playerEventLock) // lock�� �ƴϸ� ����
        {
            if (isLock) // lock ����
            {
                playerMove.playerEventLock = true;
                playerEvent += () => playerMove.playerEventLock = false;
            }

            if (targetPosition == null) // ��ġ ����
            {
                targetPosition = currentInteractablePosition;
            }

            //PlayerMove�� �����͸� ����
            playerMove.playerEvent = playerEvent;
            playerMove.isAgentCheck = isAgentCheck;

            //�÷��̾� �̵� ����
            playerMove.Event((Vector3)targetPosition);

            SaveAndLoadManager.Save(new GameData() { isStart = true, playerPosition = playerMove.transform.position, items = InventoryManager.Instance.items, topUICount = topUICount.GetAllTopUICount(), reserchPoint = ResearchManager.Instance.ReserchPoint });
        }
    }

    public void GameOver() // ���ӿ����� ����
    {
        gameOverEvent();
    }
}