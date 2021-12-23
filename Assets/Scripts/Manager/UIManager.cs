using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Image fadePanel = null; // ���̵� ��, ���̵� �ƿ� �г�
    [SerializeField] private RectTransform ui = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI

    [SerializeField] private Vector2 limitMaxPosition = Vector2.zero; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI�� �ִ� ��ġ
    [SerializeField] private Vector2 limitMinPosition = Vector2.zero; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI�� �ּ� ��ġ

    // UI ���� �ð�
    [SerializeField] private float delayTime = 0f;
    [SerializeField] private float fadeTime = 0f;

    // UI ���� ���ǵ�
    [SerializeField] private float imageOnSpeed = 0f;
    [SerializeField] private float imageOffSpeed = 0f;

    [SerializeField] private Button[] buttons = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ ��ư
    [SerializeField] private Sprite[] uiSprites = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ �̹���
    [SerializeField] private Sprite noneUISprite = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ �� �̹���

    [SerializeField] private CanvasGroup mainCanvas = null; // ���� ĵ����
    [SerializeField] private float mainCanvasTime = 0f; // ���� ĵ���� ���̵� �ð�

    [SerializeField] private Text[] topUIText; // UI ������ �ؽ�Ʈ

    [SerializeField] private Sprite[] farmFieldUI; // ��� UI
    [SerializeField] private Sprite stoneUI; // �� UI
    [SerializeField] private Sprite stoneUI2; // ��2 UI
    [SerializeField] private Sprite treeUI; // ���� UI
    [SerializeField] private Sprite tree2UI; // ����2 UI

    private Image uiImage = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI�� �̹���
    private CanvasGroup uiCanvasGroup = null; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI�� ĵ���� �׷�

    private Vector2 targetPosition = Vector2.zero; // ��ȣ�ۿ� ������ ������Ʈ�� ���������� ������ UI�� ��ǥ ��ġ

    private Tween currentTween = null; // ���� Tween

    private bool showBoat = false; // ��Ʈ UI ����
    public GameObject boatPanel = null; // ��Ʈ UI
    public GameObject clearPanel = null; // ���� Ŭ���� UI

    private GameManager gameManager;

    private void Start()
    {
        // ���� �ʱ�ȭ
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

    private void FadeIn() // ���� UI ���̵� ��
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

    private void FadeOut() // ���� UI ���̵� �ƿ�
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

    public void OnUI(UIType uiType) // �̹��� ���ϱ�
    {
        switch (uiType) // ������ ���� ������ �ٸ��� ������ switch�� if else�� ���� �����
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

    private void DefaultSprite(UIType uiType) // �⺻ UI
    {
        uiImage.sprite = uiSprites[(int)uiType];
    }

    private void StartOnUI() // �̹��� �����ֱ�
    {
        MoveUI();

        currentTween?.Complete();
        currentTween?.Kill();

        uiCanvasGroup.interactable = true;
        uiCanvasGroup.blocksRaycasts = true;

        currentTween = uiImage.DOFillAmount(1, imageOnSpeed).SetEase(Ease.Linear);
    }

    private void MoveUI() // �̹��� ������
    {
        targetPosition = Input.mousePosition;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMinPosition.x, limitMaxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitMinPosition.y, limitMaxPosition.y);

        ui.anchoredPosition = targetPosition;
    }

    public void OffUI() // �̹��� ����
    {
        StartOffUI();
    }

    private void StartOffUI() // �̹��� �ݱ�
    {
        currentTween?.Complete();
        currentTween?.Kill();

        currentTween = uiImage.DOFillAmount(0, imageOffSpeed).SetEase(Ease.Linear).OnComplete(() =>
        {
            uiCanvasGroup.interactable = false;
            uiCanvasGroup.blocksRaycasts = false;
        });  
    }

    public void SetTopUIText(TopUI topUI, int value) // UI ������ Text ����
    {
        topUIText[(int)topUI].text = value.ToString();
    }

    public void ShowBoat() // ��Ʈ â UI ����
    {
        showBoat = !showBoat;
        boatPanel.SetActive(showBoat);
    }

    public void Click() // �� ��ġ�� ��ư�� Ŭ��������
    {
        // ��ᰡ �ִ��� Ȯ��
        if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 10
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 10
            || GameManager.topUICount[TopUI.Electricity] < 20
            || !gameManager.isLight)
        {
            return;
        }

        Debug.Log("��ħ");

        //��� ���
        InventoryManager.UseItem(InventoryItem.Tree, 10);
        InventoryManager.UseItem(InventoryItem.Stone, 10);
        InventoryManager.UseItem(InventoryItem.Iron, 10);

        //���� Ŭ����
        clearPanel.SetActive(true);
        SaveAndLoadManager.DeleteFile();
    }

    //UI, ���ǵ�, ����, ������ ������ �Լ��� ������ �ڵ����� ���̵带 �������ִ� �Լ�
    public static Tween ChangeUI(CanvasGroup ui, float speed, bool isShow, Action endAction)
    {
        return ui.DOFade(isShow ? 1 : 0, speed).OnComplete(() => endAction());
    }
}