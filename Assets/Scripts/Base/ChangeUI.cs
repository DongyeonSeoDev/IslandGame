using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUI : MonoBehaviour // UI 버튼을 관리하는 클래스
{
    // UI 관련 변수
    [SerializeField] protected CanvasGroup canvasGroup;
    [SerializeField] protected Button button;
    [SerializeField] protected float speed = 0.5f;

    // 중복 Tween 방지
    protected Tween currentTween = null;

    // 현재 상태 확인
    protected bool isShow = false;

    protected virtual void Start()
    {
        button.onClick.AddListener(() =>
        {
            UIChange();
        });
    }

    protected void UIChange() // UI 상태를 바꾸는 함수
    {
        isShow = !isShow;

        if (currentTween != null && currentTween.IsActive()) // 중복 Tween 방지
        {
            currentTween.Kill();
        }

        if (isShow)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        // UIManager에서 실행
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
