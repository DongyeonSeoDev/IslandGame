using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour
{
    // 카메라 움직임 스피드
    [SerializeField] private float speed = 0f;
    [SerializeField] private float rotateSpeed = 0f;
    [SerializeField] private float scrollSpeed = 0f;

    // 카메라 움직임 제한
    [SerializeField] private Vector2 limitMax = Vector2.zero;
    [SerializeField] private Vector2 limitMin = Vector2.zero;
    [SerializeField] private float limitRotateMaxX = 0f;
    [SerializeField] private float limitRotateMinX = 0f;
    [SerializeField] private float limitScrollMax = 0f;
    [SerializeField] private float limitScrollMin = 0f;

    // 시작 애니메이션 관련 함수
    [SerializeField] private float targetFieldOfView = 0f;
    [SerializeField] private float cameraActionTime = 0f;
    [SerializeField] private float cameraStartTime = 0f;

    // 마지막 마우스 위치
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 lastRotationMousePosition = Vector3.zero;

    // 이동 관련 함수들
    private Vector3 direction = Vector3.zero;
    private Vector3 targetPosition = Vector3.zero;
    private Vector3 rotationValue = Vector3.zero;
    private Vector3 rotateDirection = Vector3.zero;
    private Vector3 targetRotatePosition = Vector3.zero;
    private Vector2 scroll = Vector2.zero;

    private float targetScrollValue = 0f;

    private Camera mainCam; //현재 카메라
    private IInteractable lastInteractable = null; //마지막 상호작용 오브젝트

    private bool gameStart = false; //게임 시작
    private float lastFieldOfView = 0f; //마지막 카메라 확대 값

    private GameManager gameManager = null;

    private void Start()
    {
        // 기본값 넣기
        mainCam = Camera.main;
        gameManager = GameManager.Instance;

        gameManager.gameOverEvent += () =>
        {
            gameStart = false;
        };

        if (!gameManager.gameData.isStart)
        {
            // 게임 시작
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
        //게임 시작시 카메라 애니메이션 실행
        mainCam.DOFieldOfView(targetFieldOfView, cameraActionTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            lastMousePosition = Input.mousePosition;
            lastRotationMousePosition = Input.mousePosition;
            gameStart = true;
        });
    }

    private void LateUpdate()
    {
        if (!gameStart) // 게임이 시작중일 때만 실행
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            ClickObject();

            // 설치가 가능한 오브젝트가 있다면
            if (gameManager.buildObject != null)
            {
                GameObject eventObject = gameManager.buildObject;
                gameManager.buildObject = null;

                InteractableObject eventInteractableObject = null;

                // 오브젝트를 위치에 설치하고, 오브젝트를 활성화한다.
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

        // 마우스 오른쪽 클릭으로 카메라를 움직임
        if (Input.GetMouseButtonDown(1))
        {
            UIManager.Instance.OffUI();

            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            MoveCamera();
        }

        // 마우스 휠 클릭으로 카메라를 회전함
        if (Input.GetMouseButtonDown(2))
        {
            UIManager.Instance.OffUI();

            lastRotationMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(2))
        {
            RotateCamera();
        }

        Zoom(); // 카메라 확대, 축소 확인
        FocusCheck(); // 마우스 위치 확인
    }

    private IInteractable CheckInteractableObject(bool isClick = false)
    {
        var ray = mainCam.ScreenPointToRay(Input.mousePosition); // ray 사용

        // UI 위에 마우스가 있다면 실행 안함
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return null;
        }

        //Raycast 사용
        IInteractable interactable = Physics.Raycast(ray, out var hit) ? hit.collider.GetComponent<IInteractable>() : null;

        // 설치 가능한 오브젝트가 있고, 설치가 가능한 위치라면 그 위치로 이동
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

        // 클릭해서 실행한 것이면 플레이어가 이동함
        if (isClick)
        {
            gameManager.PlayerMove(() =>
            {

            }, false, interactable == null ? hit.point : interactable.GetWalkPosition());
        }

        return interactable;
    }

    private void ClickObject() // 클릭시 실행
    {
        UIManager.Instance.OffUI(); //UI를 닫고

        var interactable = CheckInteractableObject(true); // 상호작용이 가능한 오브젝트가 있는지 확인

        // 상호작용이 가능하다면 상호작용
        if (!(interactable is null))
        {
            gameManager.currentInteractable = interactable;

            interactable.Interact();
        }
    }

    private void MoveCamera() // 카메라 움직임
    {
        // 카메라가 이동해야 할 방향 설정
        direction = lastMousePosition - Input.mousePosition;
        direction.z = direction.y;

        // 현재 카메라가 바라보는 방향을 기준으로 방향 설정
        rotationValue.y = transform.rotation.eulerAngles.y;

        direction = Quaternion.Euler(rotationValue) * direction;
        direction.y = 0;

        // 이동할 위치 설정
        targetPosition = transform.position + direction * (speed * mainCam.fieldOfView) * Time.deltaTime;

        // 위치 제한
        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMin.x, limitMax.x);
        targetPosition.z = Mathf.Clamp(targetPosition.z, limitMin.y, limitMax.y);

        // 이동
        transform.position = targetPosition;

        lastMousePosition = Input.mousePosition;
    }

    private void RotateCamera()
    {
        //회전할 값 설정
        rotateDirection = lastRotationMousePosition - Input.mousePosition;

        rotateDirection.z = rotateDirection.x;
        rotateDirection.x = rotateDirection.y;
        rotateDirection.y = rotateDirection.z * -1f;
        rotateDirection.z = 0;

        // 회전
        targetRotatePosition = transform.rotation.eulerAngles + rotateDirection * rotateSpeed * Time.deltaTime;

        // 회전 값 제한
        if (targetRotatePosition.x <= limitRotateMaxX || targetRotatePosition.x >= limitRotateMinX)
        {
            targetRotatePosition.z = 0f;
            transform.rotation = Quaternion.Euler(targetRotatePosition);
        }

        lastRotationMousePosition = Input.mousePosition;
    }

    private void Zoom()
    {
        // 확대, 축소 값 설정
        scroll = Input.mouseScrollDelta;
        targetScrollValue = scroll.y * scrollSpeed * Time.deltaTime;

        // 값 제한
        mainCam.fieldOfView = Mathf.Clamp(mainCam.fieldOfView - targetScrollValue, limitScrollMin, limitScrollMax);

        // 값 적용
        if (lastFieldOfView != mainCam.fieldOfView)
        {
            UIManager.Instance.OffUI();
            lastFieldOfView = mainCam.fieldOfView;
        }
    }

    private void FocusCheck()
    {
        // 마우스 위치 확인
        var interactable = CheckInteractableObject();

        if (lastInteractable != null && lastInteractable != interactable)
        {
            lastInteractable.ExitFocus(); // 마우스가 포커스에서 빠져나왔다면 실행
        }

        if (interactable != null)
        {
            interactable.EnterFocus(); // 마우스가 포커스에 들어갔다면 실행
        }

        lastInteractable = interactable;
    }
}
