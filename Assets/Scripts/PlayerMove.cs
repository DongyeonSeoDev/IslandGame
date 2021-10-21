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

            agent.SetDestination(targetPosition);
        }
    }

    private bool isDie = false;
    private bool isStart = false;

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
    }

    private void Update()
    {
        if (isDie || !isStart)
        {
            return;
        }

        animator.SetFloat(hashSpeed, agent.velocity.sqrMagnitude);
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
}
