using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Button button;
    [SerializeField] protected float speed = 0.5f;

    protected Tween currentTween = null;

    protected bool isShow = false;

    protected virtual void Start()
    {
        button.onClick.AddListener(() =>
        {
            UIChange();
        });
    }

    protected void UIChange()
    {
        isShow = !isShow;

        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        if (isShow)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        currentTween = UIManager.ChangeUI(canvasGroup, speed, isShow, () =>
        {
            if (!isShow)
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        });
    }
}
