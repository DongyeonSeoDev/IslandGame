using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RaftMove : MonoBehaviour
{
    // �÷��̾�� �����̴� ������Ʈ
    [SerializeField] private GameObject moveObject = null;
    [SerializeField] private GameObject playerObject = null;

    private PlayerMove playerMove = null; // �÷��̾� Ŭ����

    [SerializeField] private Vector3 targetPosition = Vector3.zero; // ��ǥ ��ġ
    [SerializeField] private Vector3 targetPlayerPosition = Vector3.zero; // ��ǥ �÷��̾� ��ġ

    private Vector3 currentPosition = Vector3.zero; // ���� ��ġ�� �޾Ƽ� �����ѵ� �̵��� �� ���
    private Vector3 currentPlayerPosition = Vector3.zero; // ���� �÷��̾� ��ġ�� �޾Ƽ� �����ѵ� �̵��� �� ���

    private bool isCurrentPosition = false; // ���� ��ġ ���� �Ϸ�
    private bool isMove = false; // �̵��� �ߴ��� Ȯ��
    private bool isMoving = false;

    private void Awake()
    {
        SpawnSaveObject.Instance.raftMove.Add(this);
    }

    private void Start()
    {
        // ���� �ʱ�ȭ
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void Move() // �̵� ��ư�� ��������
    {
        if (!isCurrentPosition) // ���� ��ġ ������ ���� �ʾҴٸ�
        {
            // ����
            currentPosition = transform.position;
            currentPlayerPosition = playerMove.transform.position;

            isCurrentPosition = true;
        }

        isMove = !isMove; // �̵� ����
        
        if (isMove) // true���  
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);

            isMoving = true;

            moveObject.transform.DOMove(targetPosition, 10f).OnComplete(() => // ��ǥ ��ġ�� �̵�
            {
                playerMove.transform.position = targetPlayerPosition;
                playerObject.SetActive(false);
                playerMove.gameObject.SetActive(true);

                isMoving = false;
            });
        }
        else // false���
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);

            isMoving = true;

            moveObject.transform.DOMove(currentPosition, 10f).OnComplete(() => // ������� ��ġ�� �̵�
            {
                playerMove.transform.position = currentPlayerPosition;
                playerObject.SetActive(false);
                playerMove.gameObject.SetActive(true);

                isMoving = false;
            });
        }
    }

    public RaftData GetRaftData()
    {
        return new RaftData()
        {
            position = transform.position,
            currentPosition = currentPosition,
            currentPlayerPosition = currentPlayerPosition,
            isCurrentPosition = isCurrentPosition,
            isMove = isMove,
            isMoving = isMoving
        };
    }

    public void SetRaftData(RaftData raftData)
    {
        currentPosition = raftData.currentPosition;
        currentPlayerPosition = raftData.currentPlayerPosition;
        isCurrentPosition = raftData.isCurrentPosition;
        isMove = raftData.isMove;
        isMoving = raftData.isMoving;

        if (isMoving)
        {
            isMove = !isMoving;
            transform.position = isMove ? targetPosition : currentPosition;
        }
    }
}
