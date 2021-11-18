using UnityEngine;
using UnityEngine.UI;

public abstract class Research : MonoBehaviour
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

    public int[] usePoint = null;

    public virtual void Level1()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[0];

        Debug.Log("Level1");
    }

    public virtual bool Level1Condition()
    {
        return Level == 0 && usePoint[0] <= ResearchManager.Instance.ReserchPoint;
    }

    public virtual void Level2()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[1];

        Debug.Log("Level2");
    }

    public virtual bool Level2Condition()
    {
        return Level == 1 && usePoint[1] <= ResearchManager.Instance.ReserchPoint;
    }

    public virtual void Level3()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[2];

        Debug.Log("Level3");
    }

    public virtual bool Level3Condition()
    {
        return Level == 2 && usePoint[2] <= ResearchManager.Instance.ReserchPoint;
    }
}
