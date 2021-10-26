using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image fadeInPanel = null;
    [SerializeField] private RectTransform ui = null;

    private Image uiImage = null;

    [SerializeField] private Vector2 limitMaxPosition = Vector2.zero;
    [SerializeField] private Vector2 limitMinPosition = Vector2.zero;
    
    private Vector2 targetPosition = Vector2.zero;

    [SerializeField] private float delayTime = 0f;
    [SerializeField] private float fadeInTime = 0f;
    [SerializeField] private float imageOnSpeed = 0f;
    [SerializeField] private float imageOffSpeed = 0f;
    [SerializeField] private float buttonClickDelay = 0.1f;

    private Tween currentTween = null;

    private bool isOnUI = false;

    [SerializeField] private Button treeAxeButton = null;
    [SerializeField] private Sprite[] uiSprites = null;

    [SerializeField] private CanvasGroup mainCanvas = null;
    [SerializeField] private GameObject buildPanel = null;
    [SerializeField] private Button buildPanelButton = null;
    [SerializeField] private float mainCanvasTime = 0f;

    [SerializeField] private Button[] buildButton = null;

    [SerializeField] private GameObject[] buildObject = null;

    [SerializeField] private Text[] topUIText;


    private bool isShowBuildPanel = false;

    private void Start()
    {
        uiImage = ui.GetComponent<Image>();

        Invoke("FadeIn", delayTime);

        treeAxeButton.onClick.AddListener(() =>
        {
            GameManager.Instance?.currentInteractable?.UpButtonClick();
        });

        buildPanelButton.onClick.AddListener(() =>
        {
            BuildButtonChange();
        });

        buildButton[0].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (GameManager.topUICount[TopUI.wood] < 9 || GameManager.topUICount[TopUI.stone] < 4)
            {
                return;
            }

            Debug.Log("설치");

            GameManager.topUICount[TopUI.wood] -= 10;
            GameManager.topUICount[TopUI.stone] -= 5;

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[0]);
        });

        buildButton[1].onClick.AddListener(() =>
        {
            BuildButtonChange();

            if (GameManager.topUICount[TopUI.wood] < 14 || GameManager.topUICount[TopUI.stone] < 4)
            {
                return;
            }

            Debug.Log("설치");

            GameManager.topUICount[TopUI.wood] -= 15;
            GameManager.topUICount[TopUI.stone] -= 5;

            //다른 클래스로 옳기기
            GameManager.Instance.buildObject = Instantiate(buildObject[1]);
        });
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

        fadeInPanel.DOColor(color, fadeInTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            fadeInPanel.raycastTarget = false;

            mainCanvas.interactable = true;
            mainCanvas.blocksRaycasts = true;  

            mainCanvas.DOFade(1, mainCanvasTime);
        });
    }

    public void OnUI(UIType uiType)
    {
        uiImage.sprite = uiSprites[(int)uiType];

        Invoke("StartOnUI", buttonClickDelay);
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
        currentTween = uiImage.DOFillAmount(0, imageOffSpeed).SetEase(Ease.Linear);
    }

    public void SetTopUIText(TopUI topUI, int value)
    {
        topUIText[(int)topUI].text = value.ToString();
    }
}
