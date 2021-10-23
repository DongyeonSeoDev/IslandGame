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
    [SerializeField] private float imageSpeed = 0f;

    private Tween currentTween = null;

    private bool isOnUI = false;

    private void Start()
    {
        uiImage = ui.GetComponent<Image>();

        Invoke("FadeIn", delayTime);
    }

    private void FadeIn()
    {
        Color color = Color.black;
        color.a = 0;

        fadeInPanel.DOColor(color, fadeInTime).SetEase(Ease.Linear);
    }

    public void OnUI()
    {
        MoveUI();

        currentTween = uiImage.DOFillAmount(1, imageSpeed).SetEase(Ease.Linear);
    }

    private void MoveUI()
    {
        targetPosition = Input.mousePosition;

        targetPosition.x = Mathf.Clamp(targetPosition.x, limitMinPosition.x, limitMaxPosition.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, limitMinPosition.y, limitMaxPosition.y);

        ui.anchoredPosition = targetPosition;

        isOnUI = true;
    }

    public void OffUI()
    {
        if (!isOnUI)
        {
            return;
        }

        currentTween?.Kill();
        uiImage.fillAmount = 0;

        isOnUI = false;
    }

    public void Click()
    {
        Debug.Log("Click");
    }
}
