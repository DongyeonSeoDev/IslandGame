using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RaftMove : MonoBehaviour
{
    // 플레이어랑 움직이는 오브젝트
    [SerializeField] private GameObject moveObject = null;
    [SerializeField] private GameObject playerObject = null;

    private PlayerMove playerMove = null; // 플레이어 클래스

    [SerializeField] private Vector3 targetPosition = Vector3.zero; // 목표 위치
    [SerializeField] private Vector3 targetPlayerPosition = Vector3.zero; // 목표 플레이어 위치

    private Vector3 currentPosition = Vector3.zero; // 현재 위치를 받아서 저장한뒤 이동할 때 사용
    private Vector3 currentPlayerPosition = Vector3.zero; // 현재 플레이어 위치를 받아서 저장한뒤 이동할 때 사용

    private bool isCurrentPosition = false; // 현재 위치 설정 완료
    private bool isMove = false; // 이동을 했는지 확인
    private bool isMoving = false;

    private void Awake()
    {
        SpawnSaveObject.Instance.raftMove.Add(this);
    }

    private void Start()
    {
        // 변수 초기화
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void Move() // 이동 버튼을 눌렀을때
    {
        if (!isCurrentPosition) // 현재 위치 설정을 하지 않았다면
        {
            // 설정
            currentPosition = transform.position;
            currentPlayerPosition = playerMove.transform.position;

            isCurrentPosition = true;
        }

        isMove = !isMove; // 이동 변경
        
        if (isMove) // true라면  
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);

            isMoving = true;

            moveObject.transform.DOMove(targetPosition, 10f).OnComplete(() => // 목표 위치로 이동
            {
                playerMove.transform.position = targetPlayerPosition;
                playerObject.SetActive(false);
                playerMove.gameObject.SetActive(true);

                isMoving = false;
            });
        }
        else // false라면
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);

            isMoving = true;

            moveObject.transform.DOMove(currentPosition, 10f).OnComplete(() => // 저장받은 위치로 이동
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
