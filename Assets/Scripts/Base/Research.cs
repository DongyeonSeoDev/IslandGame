using UnityEngine;
using UnityEngine.UI;

public abstract class Research : MonoBehaviour // 연구 시스템을 관리하는 클래스
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

    public int[] usePoint = null; // 업그레이드에 필요한 포인트

    public virtual void Level1() // 레벨 1 업그레이드
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[0];

        Debug.Log("Level1");
    }

    public virtual bool Level1Condition() // 레벨 1 업그레이드 조건
    {
        return Level == 0 && usePoint[0] <= ResearchManager.Instance.ReserchPoint;
    }

    public virtual void Level2() // 레벨 2 업그레이드
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[1];

        Debug.Log("Level2");
    }

    public virtual bool Level2Condition() // 레벨 2 업그레이드 조건
    {
        return Level == 1 && usePoint[1] <= ResearchManager.Instance.ReserchPoint;
    }

    public virtual void Level3() // 레벨 3 업그레이드
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[2];

        Debug.Log("Level3");
    }

    public virtual bool Level3Condition() // 레벨 3 업그레이드 조건
    {
        return Level == 2 && usePoint[2] <= ResearchManager.Instance.ReserchPoint;
    }
}
