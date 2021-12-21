using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    // ī�޶� ������ ���ǵ�
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rotateSpeed = 0f;
    [SerializeField] private float scrollSpeed = 0f;

    // ī�޶� ������ ����
    [SerializeField] private Vector2 limitMax = Vector2.zero;
    [SerializeField] private Vector2 limitMin = Vector2.zero;
    [SerializeField] private float limitRotateMaxX = 0f;
    [SerializeField] private float limitRotateMinX = 0f;
    [SerializeField] private float limitScrollMax = 0f;
    [SerializeField] private float limitScrollMin = 0f;

    // ���� �ִϸ��̼� ���� �Լ�
    [SerializeField] private float targetFieldOfView = 0f;
    [SerializeField] private float cameraActionTime = 0f;
    [SerializeField] private float cameraStartTime = 0f;

    // ������ ���콺 ��ġ
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 lastRotationMousePosition = Vector3.zero;

    // �̵� ���� �Լ���
    private Vector3 direction = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 rotationValue = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 targetRotatePosition = Vector3.zero;
    private Vector2 scroll = Vector2.zero;

    private float targetScrollValue = 0f;

    private Camera mainCam; //���� ī�޶�
    private IInteractable lastInteractable = null; //������ ��ȣ�ۿ� ������Ʈ

    private bool gameStart = false; //���� ����
    private float lastFieldOfView = 0f; //������ ī�޶� Ȯ�� ��

    private GameManager gameManager = null;

    private void Start()
    {
        // �⺻�� �ֱ�
        mainCam = Camera.main;
        gameManager = GameManager.Instance;

        gameManager.gameOverEvent += () =>
        {
            gameStart = false;
        };

        if (!gameManager.gameData.isStart)
        {
            // ���� ����
            Invoke("GameStart", gameManager.gameStartTime + cameraStartTime);
        }
        else
        {
            mainCam.fieldOfView = targetFieldOfView;
            lastMousePosition = Input.mousePosition;
            lastRotationMousePosition = Input.mousePosition;
            gameStart = true;
        }
    }

    private void GameStart()
    {
        //���� ���۽� ī�޶� �ִϸ��̼� ����
        mainCam.DOFieldOfView(targetFieldOfView, cameraActionTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            lastMousePosition = Input.mousePosition;
            lastRotationMousePosition = Input.mousePosition;
            gameStart = true;
        });
    }

    private void LateUpdate()
    {
        if (!gameStart) // ������ �������� ���� ����
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickObject();

            // ��ġ�� ������ ������Ʈ�� �ִٸ�
            if (gameManager.buildObject != null)
            {
                GameObject eventObject = gameManager.buildObject;
                gameManager.buildObject = null;

                InteractableObject eventInteractableObject = null;

                // ������Ʈ�� ��ġ�� ��ġ�ϰ�, ������Ʈ�� Ȱ��ȭ�Ѵ�.
                if (!gameManager.isBoat)
                {
                    eventInteractableObject = eventObject.GetComponent<InteractableObject>();

                    eventInteractableObject.enabled = true;

                    gameManager.PlayerMove(() =>
                    {
                        eventObject.GetComponent<Collider>().enabled = true;
                        eventObject.SetActive(true);
                    }, true, eventInteractableObject.GetWalkPosition());

                    eventObject.SetActive(false);
                }
                else
                {
                    eventInteractableObject = eventObject.GetComponentInChildren<InteractableObject>();

                    eventInteractableObject.enabled = true;

                    gameManager.PlayerMove(() =>
                    {
                        eventObject.GetComponentInChildren<Collider>().enabled = true;
                        eventObject.GetComponent<WaterFloat>().enabled = true;
                        eventObject.SetActive(true);
                    }, true, eventInteractableObject.GetWalkPosition());

                    eventObject.SetActive(false);
                }

                gameManager.isSunPower = false;
                gameManager.isBoat = false;
            }
        }

        // ���콺 ������ Ŭ������ ī�޶� ������
        if (Input.GetMouseButtonDown(1))
        {
            UIManager.Instance.OffUI();

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            MoveCamera();
        }

        // ���콺 �� Ŭ������ ī�޶� ȸ����
        if (Input.GetMouseButtonDown(2))
        {
            UIManager.Instance.OffUI();

            lastRotationMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            RotateCamera();
        }

        Zoom(); // ī�޶� Ȯ��, ��� Ȯ��
        FocusCheck(); // ���콺 ��ġ Ȯ��
    }

    private IInteractable CheckInteractableObject(bool isClick = false)
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition); // ray ���

        // UI ���� ���콺�� �ִٸ� ���� ����
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }

        //Raycast ���
        IInteractable interactable = Physics.Raycast(ray, out var hit) ? hit.collider.GetComponent<IInteractable>() : null;

        // ��ġ ������ ������Ʈ�� �ְ�, ��ġ�� ������ ��ġ��� �� ��ġ�� �̵�
        if (gameManager.buildObject != null && interactable == null)
        {
            if (gameManager.isSunPower)
            {
                gameManager.buildObject.transform.position = hit.point + new Vector3(0f, 1f, 0f);
            }
            else
            {
                gameManager.buildObject.transform.position = hit.point;
            }
        }

        // Ŭ���ؼ� ������ ���̸� �÷��̾ �̵���
        if (isClick)
        {
            gameManager.PlayerMove(() =>
            {

            }, false, interactable == null ? hit.point : interactable.GetWalkPosition());
        }

        return interactable;
    }

    private void ClickObject() // Ŭ���� ����
    {
        UIManager.Instance.OffUI(); //UI�� �ݰ�

        var interactable = CheckInteractableObject(true); // ��ȣ�ۿ��� ������ ������Ʈ�� �ִ��� Ȯ��

        // ��ȣ�ۿ��� �����ϴٸ� ��ȣ�ۿ�
        if (!(interactable is null))
        {
            gameManager.currentInteractable = interactable;

            interactable.Interact();
        }
    }

    private void MoveCamera() // ī�޶� ������
    {
        // ī�޶� �̵��ؾ� �� ���� ����
        direction = lastMousePosition - Input.mousePosition;
        direction.z = direction.y;

        // ���� ī�޶� �ٶ󺸴� ������ �������� ���� ����
        rotationValue.y = transform.rotation.eulerAngles.y;

        direction = Quaternion.Euler(rotationValue) * direction;
        direction.y = 0;

        // �̵��� ��ġ ����
        targetPosition = transform.position + direction * (speed * mainCam.fieldOfView) * Time.deltaTime;

        // ��ġ ����
        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMin.x, limitMax.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, limitMin.y, limitMax.y);

        // �̵�
        transform.position = targetPosition;

        lastMousePosition = Input.mousePosition;
    }

    private void RotateCamera()
    {
        //ȸ���� �� ����
        rotateDirection = lastRotationMousePosition - Input.mousePosition;

        rotateDirection.z = rotateDirection.x;
        rotateDirection.x = rotateDirection.y;
        rotateDirection.y = rotateDirection.z * -1f;
        rotateDirection.z = 0;

        // ȸ��
        targetRotatePosition = transform.rotation.eulerAngles + rotateDirection * rotateSpeed * Time.deltaTime;

        // ȸ�� �� ����
        if (targetRotatePosition.x <= limitRotateMaxX || targetRotatePosition.x >= limitRotateMinX)
        {
            targetRotatePosition.z = 0f;
            transform.rotation = Quaternion.Euler(targetRotatePosition);
        }

        lastRotationMousePosition = Input.mousePosition;
    }

    private void Zoom()
    {
        // Ȯ��, ��� �� ����
        scroll = Input.mouseScrollDelta;
        targetScrollValue = scroll.y * scrollSpeed * Time.deltaTime;

        // �� ����
        mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView - targetScrollValue, limitScrollMin, limitScrollMax);

        // �� ����
        if (lastFieldOfView != mainCam.fieldOfView)
        {
            UIManager.Instance.OffUI();
            lastFieldOfView = mainCam.fieldOfView;
        }
    }

    private void FocusCheck()
    {
        // ���콺 ��ġ Ȯ��
        var interactable = CheckInteractableObject();

        if (lastInteractable != null && lastInteractable != interactable)
        {
            lastInteractable.ExitFocus(); // ���콺�� ��Ŀ������ �������Դٸ� ����
        }

        if (interactable != null)
        {
            interactable.EnterFocus(); // ���콺�� ��Ŀ���� ���ٸ� ����
        }

        lastInteractable = interactable;
    }
}
