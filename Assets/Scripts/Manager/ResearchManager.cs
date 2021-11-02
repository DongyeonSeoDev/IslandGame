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

    public bool farmable { get; private set; } // 땅 설치시 확인
    public float farmValue { get; private set; } // 땅에서 얻을 시 확인
    public bool level3 { get; private set; } // TODO: Level3 기능 넣기

    public void FarmLevel1Upgrade()
    {
        farmable = true;
        farmValue = 1f;
    }

    public void FarmLevel2Upgrade()
    {
        if (farmable)
        {
            farmValue = 1.5f;
        }
    }

    public void FarmLevel3Upgrade()
    {
        level3 = true;
    }
}
