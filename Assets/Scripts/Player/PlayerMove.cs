using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent; //�׺�޽�
    private Animator animator; //�ִϸ�����

    //hash�� �̸� ����
    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashGetUp = Animator.StringToHash("getUp");
    private readonly int hashIsDie = Animator.StringToHash("isDie");

    private Vector3 gizmosPosition; // Debug�� gizmos

    private Vector3 targetPosition = Vector3.zero; // ��ǥ ��ġ
    public Vector3 TargetPosition // ��ǥ ��ġ�� ����
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

    private bool isDie = false; // �׾�����
    private bool isStart = false; // ������ ���۵Ǿ�����

    public bool playerEventLock = false; // �÷��̾� �̺�Ʈ ����
    public bool isAgentCheck = true; // �÷��̾� �׺�޽� ����

    public Action playerEvent = () =>
    {

    };

    public bool isEvent = false; // �̺�Ʈ ����

    private GameManager gameManager = null;
    private GameData gameData = null;

    private void Awake()
    {
        // ���� �ʱ�ȭ
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gameManager = GameManager.Instance;
        gameData = gameManager.gameData;

        if (!gameData.isStart)
        {
            animator.SetTrigger(hashGetUp); // �ִϸ��̼� ����
            animator.speed = 0f; // �ִϸ��̼� ����

            agent.isStopped = true;

            Invoke("StartAnimation", gameManager.gameStartTime); // ������ ���۵� �Ŀ�
        }
        else
        {
            isStart = true;
            agent.enabled = false;
            transform.position = gameData.playerPosition;
            agent.enabled = true;
        }

        gameManager.gameOverEvent += () => // ���ӿ��� �̺�Ʈ ����
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

        animator.SetFloat(hashSpeed, agent.velocity.sqrMagnitude); // �÷��̾� �ִϸ��̼� ����

        if (isEvent && !agent.pathPending && agent.remainingDistance <= 1f) // �����ߴٸ�
        {
            playerEvent(); // �÷��̾� �̺�Ʈ ����
            isEvent = false;

            if (isAgentCheck)
            {
                TargetPosition = transform.position; // ��ǥ��ġ�� ������ġ��
            }
        }

        if (!isEvent && !agent.pathPending && agent.velocity.magnitude > 0) // �̺�Ʈ�� �ƴѵ� �����̸�
        {
            TargetPosition = transform.position; // ��ǥ��ġ�� ������ġ��
        }
    }

    public void Event(Vector3 position) // �̺�Ʈ ����
    {
        TargetPosition = position;
        isEvent = true;
    }

    private void IsDie() // �׾�����
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
        animator.speed = 1f; // �ִϸ��̼� ����

        isStart = true;
        agent.isStopped = false;

        TargetPosition = transform.position;
    }
    private void OnDrawGizmos() // Gizmos �׸���
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gizmosPosition, 0.5f);
    }
}
