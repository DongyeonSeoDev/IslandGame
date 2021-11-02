using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup reserchCanvasGroup;
    [SerializeField] private Button reserchButton;
    [SerializeField] private float speed = 0.5f;

    [SerializeField] Research[] reserch;

    [SerializeField] private Text reserchPointText = null;

    private bool isShow = false;

    private void Start()
    {
        reserchButton.onClick.AddListener(() =>
        {
            isShow = !isShow;

            if (isShow)
            {
                reserchCanvasGroup.interactable = true;
                reserchCanvasGroup.blocksRaycasts = true;
            }

            UIManager.ChangeUI(reserchCanvasGroup, speed, isShow, () =>
            {
                if (!isShow)
                {
                    reserchCanvasGroup.interactable = false;
                    reserchCanvasGroup.blocksRaycasts = false;
                }
            });
        });

        for (int i = 0; i < reserch.Length; i++)
        {
            if (reserch[i].buttons.Length >= 3)
            {
                int num = i;

                reserch[num].buttons[0].onClick.AddListener(() => //��ư�� ������ �϶���?
                {
                    if (reserch[num].Level1Condition())
                    {
                        reserch[num].Level1();
                        ButtonClick(reserch[num].buttons[0]);
                    }
                });

                reserch[num].buttons[1].onClick.AddListener(() =>
                {
                    if (reserch[num].Level2Condition())
                    {
                        reserch[num].Level2();
                        ButtonClick(reserch[num].buttons[1]);
                    }
                });

                reserch[num].buttons[2].onClick.AddListener(() =>
                {
                    if (reserch[num].Level3Condition())
                    {
                        reserch[num].Level3();
                        ButtonClick(reserch[num].buttons[2]);
                    }
                });
            }
            else
            {
                Debug.LogError($"reserch[{i.ToString()}].button�� 3�� �̸� �Դϴ�.");
            }
        }
    }

    private void ButtonClick(Button button) //TODO: ��ư�� Ŭ�������� ��� �����ϴ� �Լ�
    {
        button.interactable = false;
        button.image.color = Color.yellow;
    }

    public void SetReserchPointText(int reserchPoint)
    {
        if (reserchPointText != null)
        {
            reserchPointText.text = $"���� ����Ʈ : {reserchPoint}";
        }
    }
}
