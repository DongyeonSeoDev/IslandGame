using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RaftMove : MonoBehaviour
{
    [SerializeField] private GameObject moveObject = null;
    [SerializeField] private GameObject playerObject = null;
    private PlayerMove playerMove = null;
    [SerializeField] private Vector3 targetPosition = Vector3.zero;
    [SerializeField] private Vector3 targetPlayerPosition = Vector3.zero;

    private Vector3 currentPosition = Vector3.zero;
    private Vector3 currentPlayerPosition = Vector3.zero;

    private bool isCurrentPosition = false;
    private bool isMove = false;

    private void Start()
    {
        playerMove = FindObjectOfType<PlayerMove>();
    }

    public void Move()
    {
        if (!isCurrentPosition)
        {
            currentPosition = transform.position;
            currentPlayerPosition = playerMove.transform.position;

            isCurrentPosition = true;
        }

        isMove = !isMove;

        if (isMove)
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);
            moveObject.transform.DOMove(targetPosition, 10f).OnComplete(() =>
            {
                playerMove.transform.position = targetPlayerPosition;
                playerObject.SetActive(false);
                playerMove.gameObject.SetActive(true);
            });
        }
        else
        {
            playerMove.gameObject.SetActive(false);
            playerObject.SetActive(true);
            moveObject.transform.DOMove(currentPosition, 10f).OnComplete(() =>
            {
                playerMove.transform.position = currentPlayerPosition;
                playerObject.SetActive(false);
                playerMove.gameObject.SetActive(true);
            });
        }
    }
}
