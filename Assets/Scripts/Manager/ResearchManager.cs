using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : Singleton<ResearchManager>
{
    [SerializeField] private ResearchUI researchUI = null;

    private int researchPoint = 50;

    public int ReserchPoint
    {
        get
        {
            return researchPoint;
        }

        set
        {
            researchPoint = value;

            researchUI.SetReserchPointText(researchPoint);
        }
    }

    public bool farmAble { get; private set; } // 땅 설치시 확인
    public float farmValue { get; private set; } // 땅에서 얻을 시 확인
    public bool level3 { get; private set; } // TODO: Level3 기능 넣기

    public bool stoneAble { get; private set; } // 돌 캘시 확인
    public bool ironAble { get; private set; } // 철 캘시 확인
    public bool mineLevel3 { get; private set; } // TODO: Level3 기능 넣기

    public void FarmLevel1Upgrade()
    {
        farmAble = true;
        farmValue = 1f;
    }

    public void FarmLevel2Upgrade()
    {
        if (farmAble)
        {
            farmValue = 1.5f;
        }
    }

    public void FarmLevel3Upgrade()
    {
        level3 = true;
    }

    public void MineLevel1Upgrade()
    {
        stoneAble = true;
    }

    public void MineLevel2Upgrade()
    {
        if (stoneAble)
        {
            ironAble = true;
        }
    }

    public void MineLevel3Upgrade()
    {
        if (stoneAble && ironAble)
        {
            mineLevel3 = true;
        }
    }
}
