using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResearchUI : ChangeUI // ���� ���õ� UI�� ��Ƶ� Ŭ����
{
    [SerializeField] Research[] reserch; // ���� ����
    [SerializeField] private Text reserchPointText = null; // ���� ����Ʈ �ؽ�Ʈ

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

        for (int i = 0; i < reserch.Length; i++) // ��� ������ ������
        {
            if (reserch[i].buttons.Length == 3) // ��� ������ ��ư�� �Լ� ����
            {
                int num = i;

                //��ư�� 3��

                reserch[num].buttons[0].onClick.AddListener(() => // ���� 1 ���׷��̵�
                {
                    if (reserch[num].Level1Condition())
                    {
                        reserch[num].Level1();
                        ButtonClick(reserch[num].buttons[0]);
                    }
                });

                reserch[num].buttons[1].onClick.AddListener(() => // ���� 2 ���׷��̵�
                {
                    if (reserch[num].Level2Condition())
                    {
                        reserch[num].Level2();
                        ButtonClick(reserch[num].buttons[1]);
                    }
                });

                reserch[num].buttons[2].onClick.AddListener(() => // ���� 3 ���׷��̵�
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
                Debug.LogError($"reserch[{i.ToString()}].button�� 3���� �ƴմϴ�.");
            }
        }
    }

    private void ButtonClick(Button button) // ���׷��̵尡 �Ϸ�� ��ư
    {
        button.interactable = false;
        button.image.color = Color.yellow;
    }

    public void SetReserchPointText(int reserchPoint) //���� ����Ʈ �ؽ�Ʈ ����
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
