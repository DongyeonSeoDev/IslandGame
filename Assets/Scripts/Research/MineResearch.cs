using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineResearch : Research
{
    public override void Level1()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[0];

        Debug.Log("Level1");
        ResearchManager.Instance.MineLevel1Upgrade();
    }

    public override bool Level1Condition()
    {
        return Level == 0 && usePoint[0] <= ResearchManager.Instance.ReserchPoint;
    }

    public override void Level2()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[1];

        Debug.Log("Level2");
        ResearchManager.Instance.MineLevel2Upgrade();
    }

    public override bool Level2Condition()
    {
        return Level == 1 && usePoint[1] <= ResearchManager.Instance.ReserchPoint;
    }

    public override void Level3()
    {
        Level++;
        ResearchManager.Instance.ReserchPoint -= usePoint[2];

        Debug.Log("Level3");
        ResearchManager.Instance.MineLevel3Upgrade();
    }

    public override bool Level3Condition()
    {
        return Level == 2 && usePoint[2] <= ResearchManager.Instance.ReserchPoint;
    }
}
