using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private WaitForSeconds startGameDelay;

    [SerializeField] private float targetFieldOfView = 0f;
    [SerializeField] private float cameraActionTime = 0f;
    [SerializeField] private float cameraStartTime = 0f;

    private bool gameStart = false;

    private void Start()
    {
        mainCam = Camera.main;
        startGameDelay = new WaitForSeconds(GameManager.Instance.gameStartTime + cameraStartTime);

        StartCoroutine(GameStart());
    }

    private IEnumerator GameStart()
    {
        yield return startGameDelay;

        float currentTime = 0f;
        float startFieldOfView = mainCam.fieldOfView;

        while (true)
        {
            currentTime += Time.deltaTime;

            mainCam.fieldOfView = Mathf.Lerp(startFieldOfView, targetFieldOfView, currentTime / cameraActionTime);

            if (currentTime >= cameraActionTime)
            {
                gameStart = true;
                break;
            }

            yield return null;
        }
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
        }

        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            MoveCamera();
        }

        if (Input.GetMouseButtonDown(2))
        {
            lastRotationMousePosition = Input.mousePosition;
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

        return Physics.Raycast(ray, out var hit) ? hit.collider.GetComponent<IInteractable>() : null;
    }

    private void ClickObject()
    {
        var interactable = CheckInteractableObject();

        if (!(interactable is null))
        {
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
