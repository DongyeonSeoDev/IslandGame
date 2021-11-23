using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image fadePanel = null;
    [SerializeField] private RectTransform ui = null;

    private Image uiImage = null;
    private CanvasGroup uiCanvasGroup = null;

    [SerializeField] private Vector2 limitMaxPosition = Vector2.zero;
    [SerializeField] private Vector2 limitMinPosition = Vector2.zero;

    private Vector2 targetPosition = Vector2.zero;

    [SerializeField] private float delayTime = 0f;
    [SerializeField] private float fadeTime = 0f;
    [SerializeField] private float imageOnSpeed = 0f;
    [SerializeField] private float imageOffSpeed = 0f;

    private Tween currentTween = null;

    [SerializeField] private Button[] buttons = null;
    [SerializeField] private Sprite[] uiSprites = null;
    [SerializeField] private Sprite noneUISprite = null;

    [SerializeField] private CanvasGroup mainCanvas = null;
    [SerializeField] private float mainCanvasTime = 0f;

    [SerializeField] private Text[] topUIText;

    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] speedUpSprites;

    [SerializeField] private Sprite[] farmFieldUI;
    [SerializeField] private Sprite stoneUI;
    [SerializeField] private Sprite stoneUI2;
    [SerializeField] private Sprite treeUI;
    [SerializeField] private Sprite tree2UI;

    private bool showBoat = false;
    public GameObject boatPanel = null;
    public GameObject clearPanel = null;

    private void Start()
    {
        uiImage = ui.GetComponent<Image>();
        uiCanvasGroup = ui.GetComponent<CanvasGroup>();

        Invoke("FadeIn", delayTime);

        buttons[0].onClick.AddListener(() =>
        {
            GameManager.Instance?.currentInteractable?.UpButtonClick();
        });

        buttons[1].onClick.AddListener(() =>
        {
            GameManager.Instance?.currentInteractable?.DownButtonClick();
        });

        buttons[2].onClick.AddListener(() =>
        {
            GameManager.Instance?.currentInteractable?.RightButtonClick();
        });

        buttons[3].onClick.AddListener(() =>
        {
            GameManager.Instance?.currentInteractable?.LeftButtonClick();
        });

        GameManager.Instance.gameOverEvent += () =>
        {
            Invoke("FadeOut", delayTime);
        };
    }

    private void FadeIn()
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

    private void FadeOut()
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
        switch (uiType)
        {
            case UIType.SunPower:

                if (GameManager.Instance.currentInteractable.GetUseSunPower())
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.FarmField:

                if (!GameManager.Instance.currentInteractable.GetSeed())
                {
                    uiImage.sprite = farmFieldUI[0];
                }
                else if (GameManager.Instance.currentInteractable.GetComplete())
                {
                    uiImage.sprite = farmFieldUI[2];
                }
                else if (!GameManager.Instance.currentInteractable.GetWater())
                {
                    uiImage.sprite = farmFieldUI[1];
                }
                else if (GameManager.Instance.currentInteractable.GetSeed() && GameManager.Instance.currentInteractable.GetWater())
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
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetStone() ? stoneUI2 : noneUISprite;
                }
                else
                {
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetStone() ? stoneUI : uiSprites[(int)uiType];
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
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetTree() ? tree2UI : noneUISprite;
                }
                else
                {
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetTree() ? treeUI : uiSprites[(int)uiType];
                }

                break;

            default:
                DefaultSprite(uiType);
                break;
        }

        StartOnUI();
    }

    private void DefaultSprite(UIType uiType)
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

    public void SetTopUIText(TopUI topUI, int value)
    {
        topUIText[(int)topUI].text = value.ToString();
    }

    public void ShowBoat()
    {
        showBoat = !showBoat;
        boatPanel.SetActive(showBoat);
    }

    public void Click()
    {
        if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 10
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 10
            || GameManager.topUICount[TopUI.Electricity] < 20
            || !GameManager.Instance.isLight)
        {
            return;
        }

        Debug.Log("고침");

        InventoryManager.UseItem(InventoryItem.Tree, 10);
        InventoryManager.UseItem(InventoryItem.Stone, 10);
        InventoryManager.UseItem(InventoryItem.Iron, 10);

        clearPanel.SetActive(true);
    }

    public static Tween ChangeUI(CanvasGroup ui, float speed, bool isShow, Action endAction)
    {
        return ui.DOFade(isShow ? 1 : 0, speed).OnComplete(() => endAction());
    }
}