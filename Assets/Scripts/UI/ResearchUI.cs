using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUI : ChangeUI
{
    [SerializeField] Research[] reserch;
    [SerializeField] private Text reserchPointText = null;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < reserch.Length; i++)
        {
            if (reserch[i].buttons.Length == 3)
            {
                int num = i;

                //버튼은 3개

                reserch[num].buttons[0].onClick.AddListener(() =>
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
                Debug.LogError($"reserch[{i.ToString()}].button이 3개가 아닙니다.");
            }
        }
    }

    private void ButtonClick(Button button) //TODO: 버튼을 클릭했을때 모양 설정하는 함수
    {
        button.interactable = false;
        button.image.color = Color.yellow;
    }

    public void SetReserchPointText(int reserchPoint)
    {
        if (reserchPointText != null)
        {
            reserchPointText.text = $"연구 포인트 : {reserchPoint}";
        }
    }
}
