using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour // UI ��ư�� �����ϴ� Ŭ����
{
    // UI ���� ����
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Button button;
    [SerializeField] protected float speed = 0.5f;

    // �ߺ� Tween ����
    protected Tween currentTween = null;

    // ���� ���� Ȯ��
    protected bool isShow = false;

    protected virtual void Start()
    {
        button.onClick.AddListener(() =>
        {
            UIChange();
        });
    }

    protected void UIChange() // UI ���¸� �ٲٴ� �Լ�
    {
        isShow = !isShow;

        if (currentTween != null && currentTween.IsActive()) // �ߺ� Tween ����
        {
            currentTween.Kill();
        }

        if (isShow)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        // UIManager���� ����
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
