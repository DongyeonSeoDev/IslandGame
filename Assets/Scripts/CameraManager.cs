using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rotateSpeed = 0f;
    [SerializeField] private float scrollSpeed = 0f;

    [SerializeField] private Vector2 limitMax = Vector2.zero;
    [SerializeField] private Vector2 limitMin = Vector2.zero;
    [SerializeField] private float limitRotateMaxX = 0f;
    [SerializeField] private float limitRotateMinX = 0f;
    [SerializeField] private float limitScrollMax = 0f;
    [SerializeField] private float limitScrollMin = 0f;

    private Camera mainCam;

    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 lastRotationMousePosition = Vector3.zero;

    private Vector3 direction = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 rotationValue = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 targetRotatePosition = Vector3.zero;
    private Vector2 scroll = Vector2.zero;

    private float targetScrollValue = 0f;

    private IInteractable lastInteractable = null;

    [SerializeField] private float targetFieldOfView = 0f;
    [SerializeField] private float cameraActionTime = 0f;
    [SerializeField] private float cameraStartTime = 0f;

    private bool gameStart = false;

    private float lastFieldOfView = 0f;

    private void Start()
    {
        mainCam = Camera.main;

        Invoke("GameStart", GameManager.Instance.gameStartTime + cameraStartTime);

        GameManager.Instance.gameOverEvent += () =>
        {
            gameStart = false;
        };
    }

    private void GameStart()
    {
        mainCam.DOFieldOfView(targetFieldOfView, cameraActionTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            gameStart = true;
        });
    }

    private void LateUpdate()
    {
        if (!gameStart)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickObject();

            //TODO

            if (GameManager.Instance.buildObject != null)
            {
                GameObject eventObject = GameManager.Instance.buildObject;
                GameManager.Instance.buildObject = null;

                eventObject.SetActive(false);
                GameManager.Instance.currentInteractablePosition = eventObject.transform.position;

                if (!GameManager.Instance.isBoat)
                {
                    GameManager.Instance.PlayerMove(() =>
                    {
                        eventObject.SetActive(true);
                        eventObject.GetComponent<Collider>().enabled = true;
                        eventObject.GetComponent<InteractableObject>().enabled = true;
                    }, true);
                }
                else
                {
                    GameManager.Instance.PlayerMove(() =>
                    {
                        eventObject.SetActive(true);
                        eventObject.GetComponentInChildren<Collider>().enabled = true;
                        eventObject.GetComponentInChildren<InteractableObject>().enabled = true;
                    }, true);
                }

                GameManager.Instance.isSunPower = false;
                GameManager.Instance.isBoat = false;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
            UIManager.Instance.OffUI();
        }

        if (Input.GetMouseButton(1))
        {
            MoveCamera();
        }

        if (Input.GetMouseButtonDown(2))
        {
            lastRotationMousePosition = Input.mousePosition;
            UIManager.Instance.OffUI();
        }

        if (Input.GetMouseButton(2))
        {
            RotateCamera();
        }

        Zoom();
        FocusCheck();
    }

    private IInteractable CheckInteractableObject()
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition);

        IInteractable interactable = Physics.Raycast(ray, out var hit) ? hit.collider.GetComponent<IInteractable>() : null;

        if (GameManager.Instance.buildObject != null)
        {
            if (GameManager.Instance.isSunPower)
            {
                GameManager.Instance.buildObject.transform.position = hit.point + new Vector3(0f, 1f, 0f);
            }
            else
            {
                GameManager.Instance.buildObject.transform.position = hit.point;
            }
        }

        return interactable;
    }

    private void ClickObject()
    {
        UIManager.Instance.OffUI();

        var interactable = CheckInteractableObject();

        if (!(interactable is null))
        {
            GameManager.Instance.currentInteractable = interactable;

            interactable.Interact();
        }
    }

    private void MoveCamera()
    {
        direction = lastMousePosition - Input.mousePosition;
        direction.z = direction.y;

        rotationValue.y = transform.rotation.eulerAngles.y;

        direction = Quaternion.Euler(rotationValue) * direction;
        direction.y = 0;

        targetPosition = transform.position + direction * (speed * mainCam.fieldOfView) * Time.deltaTime;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMin.x, limitMax.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, limitMin.y, limitMax.y);

        transform.position = targetPosition;

        lastMousePosition = Input.mousePosition;
    }

    private void RotateCamera()
    {
        rotateDirection = lastRotationMousePosition - Input.mousePosition;

        rotateDirection.z = rotateDirection.x;
        rotateDirection.x = rotateDirection.y;
        rotateDirection.y = rotateDirection.z * -1f;
        rotateDirection.z = 0;

        targetRotatePosition = transform.rotation.eulerAngles + rotateDirection * rotateSpeed * Time.deltaTime;

        if (targetRotatePosition.x <= limitRotateMaxX || targetRotatePosition.x >= limitRotateMinX)
        {
            targetRotatePosition.z = 0f;
            transform.rotation = Quaternion.Euler(targetRotatePosition);
        }

        lastRotationMousePosition = Input.mousePosition;
    }

    private void Zoom()
    {
        scroll = Input.mouseScrollDelta;
        targetScrollValue = scroll.y * scrollSpeed * Time.deltaTime;

        mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView - targetScrollValue, limitScrollMin, limitScrollMax);

        if (lastFieldOfView != mainCam.fieldOfView)
        {
            UIManager.Instance.OffUI();
            lastFieldOfView = mainCam.fieldOfView;
        }
    }

    private void FocusCheck()
    {
        var interactable = CheckInteractableObject();

        if (!(lastInteractable is null) && lastInteractable != interactable)
        {
            lastInteractable.ExitFocus();
        }

        if (!(interactable is null))
        {
            interactable.EnterFocus();
        }

        lastInteractable = interactable;
    }
}
