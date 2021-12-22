using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent; //네브메쉬
    private Animator animator; //애니메이터

    //hash로 미리 정의
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashGetUp = Animator.StringToHash("getUp");
    private readonly int hashIsDie = Animator.StringToHash("isDie");

    private Vector3 gizmosPosition; // Debug용 gizmos

    private Vector3 targetPosition = Vector3.zero; // 목표 위치
    public Vector3 TargetPosition // 목표 위치를 설정
    {
        get
        {
            return targetPosition;
        }

        set
        {
            targetPosition = value;
            gizmosPosition = targetPosition;

            agent.SetDestination(targetPosition);
        }
    }

    private bool isDie = false; // 죽었는지
    private bool isStart = false; // 게임이 시작되었는지

    public bool playerEventLock = false; // 플레이어 이벤트 관리
    public bool isAgentCheck = true; // 플레이어 네브메쉬 관리

    public Action playerEvent = () =>
    {

    };

    public bool isEvent = false; // 이벤트 관리

    private GameManager gameManager = null;
    private GameData gameData = null;

    private void Awake()
    {
        // 변수 초기화
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameData = gameManager.gameData;

        if (!gameData.isStart)
        {
            animator.SetTrigger(hashGetUp); // 애니메이션 실행
            animator.speed = 0f; // 애니메이션 멈춤

            agent.isStopped = true;

            Invoke("StartAnimation", gameManager.gameStartTime); // 게임이 시작된 후에
        }
        else
        {
            isStart = true;
            agent.enabled = false;
            transform.position = gameData.playerPosition;
            agent.enabled = true;
        }

        gameManager.gameOverEvent += () => // 게임오버 이벤트 설정
        {
            IsDie();
        };
    }

    private void Update()
    {
        if (isDie || !isStart)
        {
            return;
        }

        animator.SetFloat(hashSpeed, agent.velocity.sqrMagnitude); // 플레이어 애니메이션 설정

        if (isEvent && !agent.pathPending && agent.remainingDistance <= 1f) // 도착했다면
        {
            playerEvent(); // 플레이어 이벤트 실행
            isEvent = false;

            if (isAgentCheck)
            {
                TargetPosition = transform.position; // 목표위치는 현재위치로
            }
        }

        if (!isEvent && !agent.pathPending && agent.velocity.magnitude > 0) // 이벤트가 아닌데 움직이면
        {
            TargetPosition = transform.position; // 목표위치는 현재위치로
        }
    }

    public void Event(Vector3 position) // 이벤트 실행
    {
        TargetPosition = position;
        isEvent = true;
    }

    private void IsDie() // 죽었을때
    {
        if (isDie || !isStart)
        {
            return;
        }

        animator.SetTrigger(hashIsDie);
        isDie = true;
        isStart = false;
    }

    private void StartAnimation()
    {
        animator.speed = 1f; // 애니메이션 시작

        isStart = true;
        agent.isStopped = false;

        TargetPosition = transform.position;
    }
    private void OnDrawGizmos() // Gizmos 그리기
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gizmosPosition, 0.5f);
    }
}
