using UnityEngine;
using UnityEngine.UI;

public abstract class Research : MonoBehaviour // ���� �ý����� �����ϴ� Ŭ����
{
    public Button[] buttons;

    private int level = 0;

    public int Level
    {
        get
        {
            return level;
        }

        set
        {
            level = value;
        }
    }

    public int[] usePoint = null; // ���׷��̵忡 �ʿ��� ����Ʈ

    public virtual void Level1() // ���� 1 ���׷��̵�
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[0];

        Debug.Log("Level1");
    }

    public virtual void Level2() // ���� 2 ���׷��̵�
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[1];

        Debug.Log("Level2");
    }

    public virtual void Level3() // ���� 3 ���׷��̵�
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[2];

        Debug.Log("Level3");
    }
}
