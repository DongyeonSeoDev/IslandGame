using UnityEngine;
using UnityEngine.UI;

public class Tooltip : Singleton<Tooltip>
{
    [SerializeField]
    private RectTransform canvasRectTrm;

    [SerializeField]  
    private Text text;

    [SerializeField]
    private RectTransform backgroundRectTrm;

    [SerializeField]
    private Image backgroundImage;

    private RectTransform rectTrm;
    private float timer = -1f;

    protected override void Awake()
    {
        base.Awake();

        rectTrm = GetComponent<RectTransform>();

        Hide();
    }

    private void Update()
    {
        HandleFollowMouse();

        // 타이머 처리
        if (timer != -1f)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, 60f);

            if (timer == 0)
            {
                timer = -1f;
                Hide();
            }
        }
    }

    private void HandleFollowMouse()
    {
        Vector2 anchoredPos = Input.mousePosition / canvasRectTrm.localScale.x;

        if (anchoredPos.x + backgroundRectTrm.rect.width > canvasRectTrm.rect.width)
        {
            anchoredPos.x = canvasRectTrm.rect.width - backgroundRectTrm.rect.width;
        }

        if (anchoredPos.y + backgroundRectTrm.rect.height > canvasRectTrm.rect.height)
        {
            anchoredPos.y = canvasRectTrm.rect.height - backgroundRectTrm.rect.height;
        }

        rectTrm.anchoredPosition = anchoredPos;
    }

    private void SetText(string tooltipText)
    {
        text.text = tooltipText;

        backgroundImage.color = Color.clear;
        text.color = Color.clear;

        Invoke("TextChange", 0.1f);
    }

    private void TextChange()
    {
        Vector2 textSize = text.rectTransform.sizeDelta;
        Vector2 padding = new Vector2(20, 20);

        backgroundRectTrm.sizeDelta = textSize + padding;

        Invoke("ColorChange", 0.1f);
    }

    private void ColorChange()
    {
        backgroundImage.color = Color.black;
        text.color = Color.white;
    }

    public void Show(string tooltipText, float timer = -1f)
    {
        this.timer = timer;

        gameObject.SetActive(true);
        SetText(tooltipText);

        HandleFollowMouse();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}