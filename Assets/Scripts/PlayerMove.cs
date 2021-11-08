using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private readonly int hashSpeed = Animator.StringToHash("speed");
    private readonly int hashGetUp = Animator.StringToHash("getUp");
    private readonly int hashIsDie = Animator.StringToHash("isDie");

    private Vector3 gizmosPosition;

    private Vector3 targetPosition = Vector3.zero;
    public Vector3 TargetPosition
    {
        get
        {
            return targetPosition;
        }

        set
        {
            targetPosition = value;
            //targetPosition -= transform.forward;
            targetPosition.y = transform.position.y;

            gizmosPosition = targetPosition;

            agent.SetDestination(targetPosition);
        }
    }

    private bool isDie = false;
    private bool isStart = false;

    public bool playerEventLock = false;
    public bool isAgentCheck = true;

    public Action playerEvent = () =>
    {

    };

    public bool isEvent = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.SetTrigger(hashGetUp);
        animator.speed = 0f;

        agent.isStopped = true;

        Invoke("StartAnimation", GameManager.Instance.gameStartTime);

        GameManager.Instance.gameOverEvent += () =>
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

        animator.SetFloat(hashSpeed, agent.velocity.sqrMagnitude);

        if (isEvent && !agent.pathPending && agent.remainingDistance <= 1f)
        {
            playerEvent();
            isEvent = false;

            if (isAgentCheck)
            {
                TargetPosition = transform.position;
            }
        }

        if (!isEvent && !agent.pathPending && agent.velocity.magnitude > 0)
        {
            TargetPosition = transform.position;
        }
    }

    public void Event(Vector3 position)
    {
        TargetPosition = position;
        isEvent = true;
    }

    private void IsDie()
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
        animator.speed = 1f;

        isStart = true;
        agent.isStopped = false;

        TargetPosition = transform.position;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(gizmosPosition, 0.5f);
    }
}
