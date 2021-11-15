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
    [SerializeField] private float buttonClickDelay = 0.1f;

    private Tween currentTween = null;

    private bool isOnUI = false;

    [SerializeField] private Button[] buttons = null;
    [SerializeField] private Sprite[] uiSprites = null;
    [SerializeField] private Sprite noneUISprite = null;

    [SerializeField] private CanvasGroup mainCanvas = null;
    [SerializeField] private GameObject buildPanel = null;
    [SerializeField] private Button buildPanelButton = null;
    [SerializeField] private float mainCanvasTime = 0f;

    [SerializeField] private Button[] buildButton = null;
    [SerializeField] private GameObject[] buildObject = null;
    [SerializeField] private Text[] topUIText;

    [SerializeField] private Button normalButton;
    [SerializeField] private Button speedUpButton;
    [SerializeField] private Sprite[] normalSprites;
    [SerializeField] private Sprite[] speedUpSprites;

    [SerializeField] private Sprite[] farmFieldUI;
    [SerializeField] private Sprite stoneUI;
    [SerializeField] private Sprite stoneUI2;
    [SerializeField] private Sprite treeUI;

    private float[] warningTime = new float[6];

    private bool isShowBuildPanel = false;

    private bool showBoat = false;
    public GameObject boatPanel = null;
    public GameObject clearPanel = null;

    private void Start()
    {
        uiImage = ui.GetComponent<Image>();
        uiCanvasGroup = ui.GetComponent<CanvasGroup>();

        Invoke("FadeIn", delayTime);

        Array.ForEach(warningTime, x => x = -1);

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

        buildPanelButton.onClick.AddListener(() =>
        {
            BuildButtonChange();
        });

        buildButton[0].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                return;
            }

            Debug.Log("설치");

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[0]);
        });

        buildButton[1].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15 
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.electricity] < 20)
            {
                return;
            }

            Debug.Log("설치");

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);
            GameManager.topUICount[TopUI.electricity] -= 20;

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[1]);
        });

        buildButton[2].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (!InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                return;
            }

            Debug.Log("설치");

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2
            || GameManager.topUICount[TopUI.electricity] < 20)
            {
                return;
            }

            Debug.Log("설치");

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[3]);
            GameManager.Instance.isSunPower = true;
        });

        buildButton[4].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (!InventoryManager.UseItem(InventoryItem.Tree, 10))
            {
                return;
            }

            Debug.Log("설치");

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[4]);
            GameManager.Instance.isBoat = true;
        });

        buildButton[5].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.electricity] < 20)
            {
                return;
            }

            Debug.Log("설치");

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[5]);
        });

        normalButton.onClick.AddListener(() =>
        {
            if (GameManager.Instance.isSpeedUp)
            {
                GameManager.Instance.isSpeedUp = false;

                normalButton.image.sprite = normalSprites[1];
                speedUpButton.image.sprite = speedUpSprites[0];
            }
        });

        speedUpButton.onClick.AddListener(() =>
        {
            if (!GameManager.Instance.isSpeedUp)
            {
                GameManager.Instance.isSpeedUp = true;

                normalButton.image.sprite = normalSprites[0];
                speedUpButton.image.sprite = speedUpSprites[1];
            }
        });

        GameManager.Instance.gameOverEvent += () =>
        {
            Invoke("FadeOut", delayTime);
        };
    }

    private void BuildButtonChange()
    {
        isShowBuildPanel = !isShowBuildPanel;
        buildPanel.SetActive(isShowBuildPanel);
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

    public void OnUI(UIType uiType)
    {
        switch (uiType)
        {
            case UIType.sunPower:

                if (GameManager.Instance.currentInteractable.GetUseSunPower())
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.farmField:

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

            case UIType.stone:

                if (!ResearchManager.Instance.stoneAble)
                {
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetStone() ? stoneUI2 : noneUISprite;
                }
                else
                {
                    uiImage.sprite = GameManager.Instance.currentInteractable.GetStone() ? stoneUI : uiSprites[(int)uiType];
                }

                break;

            case UIType.iron:

                if (!ResearchManager.Instance.ironAble)
                {
                    uiImage.sprite = noneUISprite;
                }
                else
                {
                    DefaultSprite(uiType);
                }

                break;

            case UIType.tree:

                if (GameManager.Instance.currentInteractable.GetTree())
                {
                    uiImage.sprite = treeUI;
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

        Invoke("StartOnUI", buttonClickDelay);
    }

    private void DefaultSprite(UIType uiType)
    {
        uiImage.sprite = uiSprites[(int)uiType];
    }

    private void StartOnUI()
    {
        if (isOnUI)
        {
            return;
        }

        MoveUI();

        isOnUI = true;

        currentTween?.Complete();
        currentTween?.Kill();

        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;

        currentTween = uiImage.DOFillAmount(1, imageOnSpeed).SetEase(Ease.Linear);
    }

    private void MoveUI()
    {
        targetPosition = Input.mousePosition;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMinPosition.x, limitMaxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitMinPosition.y, limitMaxPosition.y);

        ui.anchoredPosition = targetPosition;
    }

    public void OffUI()
    {
        Invoke("StartOffUI", buttonClickDelay);
    }

    private void StartOffUI()
    {
        if (!isOnUI)
        {
            return;
        }

        isOnUI = false;

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
            || GameManager.topUICount[TopUI.electricity] < 20
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

    public static void ChangeUI(CanvasGroup ui, float speed, bool isShow, Action endAction)
    {
        ui.DOFade(isShow ? 1 : 0, speed).OnComplete(() => endAction());
    }
}