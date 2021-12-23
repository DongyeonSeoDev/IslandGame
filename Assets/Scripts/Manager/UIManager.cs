using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image fadePanel = null; // 페이드 인, 페이드 아웃 패널
    [SerializeField] private RectTransform ui = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI

    [SerializeField] private Vector2 limitMaxPosition = Vector2.zero; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI의 최대 위치
    [SerializeField] private Vector2 limitMinPosition = Vector2.zero; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI의 최소 위치

    // UI 관련 시간
    [SerializeField] private float delayTime = 0f;
    [SerializeField] private float fadeTime = 0f;

    // UI 관련 스피드
    [SerializeField] private float imageOnSpeed = 0f;
    [SerializeField] private float imageOffSpeed = 0f;

    [SerializeField] private Button[] buttons = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 버튼
    [SerializeField] private Sprite[] uiSprites = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 이미지
    [SerializeField] private Sprite noneUISprite = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 빈 이미지

    [SerializeField] private CanvasGroup mainCanvas = null; // 메인 캔버스
    [SerializeField] private float mainCanvasTime = 0f; // 메인 캔버스 페이드 시간

    [SerializeField] private Text[] topUIText; // UI 아이템 텍스트

    [SerializeField] private Sprite[] farmFieldUI; // 농사 UI
    [SerializeField] private Sprite stoneUI; // 돌 UI
    [SerializeField] private Sprite stoneUI2; // 돌2 UI
    [SerializeField] private Sprite treeUI; // 나무 UI
    [SerializeField] private Sprite tree2UI; // 나무2 UI

    private Image uiImage = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI의 이미지
    private CanvasGroup uiCanvasGroup = null; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI의 캔버스 그룹

    private Vector2 targetPosition = Vector2.zero; // 상호작용 가능한 오브젝트를 선택했을때 나오는 UI의 목표 위치

    private Tween currentTween = null; // 현재 Tween

    private bool showBoat = false; // 보트 UI 상태
    public GameObject boatPanel = null; // 보트 UI
    public GameObject clearPanel = null; // 게임 클리어 UI

    private GameManager gameManager;

    private void Start()
    {
        // 변수 초기화
        gameManager = GameManager.Instance;

        uiImage = ui.GetComponent<Image>();
        uiCanvasGroup = ui.GetComponent<CanvasGroup>();

        buttons[0].onClick.AddListener(() =>
        {
            gameManager?.currentInteractable?.UpButtonClick();
        });

        buttons[1].onClick.AddListener(() =>
        {
            gameManager?.currentInteractable?.DownButtonClick();
        });

        buttons[2].onClick.AddListener(() =>
        {
            gameManager?.currentInteractable?.RightButtonClick();
        });

        buttons[3].onClick.AddListener(() =>
        {
            gameManager?.currentInteractable?.LeftButtonClick();
        });

        gameManager.gameOverEvent += () =>
        {
            Invoke("FadeOut", delayTime);
        };

        if (gameManager.gameData.isStart)
        {
            Color color = Color.black;
            color.a = 0;

            fadePanel.raycastTarget = false;
            mainCanvas.interactable = true;
            mainCanvas.blocksRaycasts = true;

            fadePanel.color = color;
            mainCanvas.alpha = 1;
        }
        else
        {
            Invoke("FadeIn", delayTime);
        }
    }

    private void FadeIn() // 메인 UI 페이드 인
    {
        Color color = Color.black;
        color.a = 0;

        fadePanel.DOColor(color, fadeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadePanel.raycastTarget = false;

            mainCanvas.interactable = true;
            mainCanvas.blocksRaycasts = true;

            mainCanvas.DOFade(1, mainCanvasTime);
        });
    }

    private void FadeOut() // 메인 UI 페이드 아웃
    {
        Color color = Color.black;

        mainCanvas.alpha = 0;

        mainCanvas.interactable = false;
        mainCanvas.blocksRaycasts = false;

        fadePanel.DOColor(color, fadeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadePanel.raycastTarget = true;
        });
    }

    public void OnUI(UIType uiType) // 이미지 정하기
    {
        switch (uiType) // 아이템 별로 조건이 다르기 때문에 switch와 if else를 같이 사용함
        {
            case UIType.SunPower:

                if (gameManager.currentInteractable.GetUseSunPower())
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.FarmField:

                if (!gameManager.currentInteractable.GetSeed())
                {
                    uiImage.sprite = farmFieldUI[0];
                }
                else if (gameManager.currentInteractable.GetComplete())
                {
                    uiImage.sprite = farmFieldUI[2];
                }
                else if (!gameManager.currentInteractable.GetWater())
                {
                    uiImage.sprite = farmFieldUI[1];
                }
                else if (gameManager.currentInteractable.GetSeed() && gameManager.currentInteractable.GetWater())
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.Stone:

                if (!ResearchManager.Instance.isUsePickax || InventoryManager.GetItemCount(InventoryItem.Pickax) <= 0)
                {
                    uiImage.sprite = gameManager.currentInteractable.GetStone() ? stoneUI2 : noneUISprite;
                }
                else
                {
                    uiImage.sprite = gameManager.currentInteractable.GetStone() ? stoneUI : uiSprites[(int)uiType];
                }

                break;

            case UIType.Iron:

                if (!ResearchManager.Instance.isUseStrongPickax || InventoryManager.GetItemCount(InventoryItem.StrongPickaxe) <= 0)
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.Tree:

                if (!ResearchManager.Instance.isUseAxe || InventoryManager.GetItemCount(InventoryItem.Axe) <= 0)
                {
                    uiImage.sprite = gameManager.currentInteractable.GetTree() ? tree2UI : noneUISprite;
                }
                else
                {
                    uiImage.sprite = gameManager.currentInteractable.GetTree() ? treeUI : uiSprites[(int)uiType];
                }

                break;

            case UIType.LightHouse:

                if (GameManager.Instance.isLight)
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            default:
                DefaultSprite(uiType);
                break;
        }

        StartOnUI();
    }

    private void DefaultSprite(UIType uiType) // 기본 UI
    {
        uiImage.sprite = uiSprites[(int)uiType];
    }

    private void StartOnUI() // 이미지 보여주기
    {
        MoveUI();

        currentTween?.Complete();
        currentTween?.Kill();

        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;

        currentTween = uiImage.DOFillAmount(1, imageOnSpeed).SetEase(Ease.Linear);
    }

    private void MoveUI() // 이미지 움직임
    {
        targetPosition = Input.mousePosition;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMinPosition.x, limitMaxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitMinPosition.y, limitMaxPosition.y);

        ui.anchoredPosition = targetPosition;
    }

    public void OffUI() // 이미지 끄기
    {
        StartOffUI();
    }

    private void StartOffUI() // 이미지 닫기
    {
        currentTween?.Complete();
        currentTween?.Kill();

        currentTween = uiImage.DOFillAmount(0, imageOffSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        });  
    }

    public void SetTopUIText(TopUI topUI, int value) // UI 아이템 Text 변경
    {
        topUIText[(int)topUI].text = value.ToString();
    }

    public void ShowBoat() // 보트 창 UI 설정
    {
        showBoat = !showBoat;
        boatPanel.SetActive(showBoat);
    }

    public void Click() // 배 고치기 버튼을 클릭했을때
    {
        // 재료가 있는지 확인
        if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 10
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 10
            || GameManager.topUICount[TopUI.Electricity] < 20
            || !gameManager.isLight)
        {
            return;
        }

        Debug.Log("고침");

        //재료 사용
        InventoryManager.UseItem(InventoryItem.Tree, 10);
        InventoryManager.UseItem(InventoryItem.Stone, 10);
        InventoryManager.UseItem(InventoryItem.Iron, 10);

        //게임 클리어
        clearPanel.SetActive(true);
        SaveAndLoadManager.DeleteFile();
    }

    //UI, 스피드, 상태, 끝나고 실행할 함수를 넣으면 자동으로 페이드를 실행해주는 함수
    public static Tween ChangeUI(CanvasGroup ui, float speed, bool isShow, Action endAction)
    {
        return ui.DOFade(isShow ? 1 : 0, speed).OnComplete(() => endAction());
    }
}