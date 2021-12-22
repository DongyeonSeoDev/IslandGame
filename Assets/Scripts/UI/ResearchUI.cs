using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUI : ChangeUI // 연구 관련된 UI를 모아둔 클래스
{
    [SerializeField] Research[] reserch; // 연구 종류
    [SerializeField] private Text reserchPointText = null; // 연구 포인트 텍스트

    private GameManager gameManager = null;
    private GameData gameData = null;

    protected override void Start()
    {
        base.Start();

        gameManager = GameManager.Instance;
        gameData = gameManager.gameData;

        if (gameData.isStart)
        {
            for (int i = 0; i < reserch.Length; i++)
            {
                if (gameData.reserchLevel[i] >= 1)
                {
                    reserch[i].Level1();
                    ButtonClick(reserch[i].buttons[0]);

                    if (gameData.reserchLevel[i] >= 2)
                    {
                        reserch[i].Level2();
                        ButtonClick(reserch[i].buttons[1]);

                        if (gameData.reserchLevel[i] >= 3)
                        {
                            reserch[i].Level3();
                            ButtonClick(reserch[i].buttons[2]);
                        }
                    }
                }
            }
        }

        if (gameData.isStart)
        {
            ResearchManager.Instance.ReserchPoint = GameManager.Instance.gameData.reserchPoint;
        }

        for (int i = 0; i < reserch.Length; i++) // 모든 연구를 가져옴
        {
            if (reserch[i].buttons.Length == 3) // 모든 연구의 버튼에 함수 연결
            {
                int num = i;

                //버튼은 3개

                reserch[num].buttons[0].onClick.AddListener(() => // 레벨 1 업그레이드
                {
                    if (reserch[num].Level1Condition())
                    {
                        reserch[num].Level1();
                        ButtonClick(reserch[num].buttons[0]);
                    }
                });

                reserch[num].buttons[1].onClick.AddListener(() => // 레벨 2 업그레이드
                {
                    if (reserch[num].Level2Condition())
                    {
                        reserch[num].Level2();
                        ButtonClick(reserch[num].buttons[1]);
                    }
                });

                reserch[num].buttons[2].onClick.AddListener(() => // 레벨 3 업그레이드
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

    private void ButtonClick(Button button) // 업그레이드가 완료된 버튼
    {
        button.interactable = false;
        button.image.color = Color.yellow;
    }

    public void SetReserchPointText(int reserchPoint) //연구 포인트 텍스트 설정
    {
        if (reserchPointText != null)
        {
            reserchPointText.text = reserchPoint.ToString();
        }
    }

    public int[] GetReserchData()
    {
        int[] reserchData = new int[reserch.Length];

        for (int i = 0; i < reserch.Length; i++)
        {
            reserchData[i] = reserch[i].Level;
        }

        return reserchData;
    }
}
