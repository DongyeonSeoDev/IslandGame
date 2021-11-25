using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : Singleton<ResearchManager>
{
    [SerializeField] private ResearchUI researchUI = null; //연구 UI

    private int researchPoint = 0; //연구 포인트

    //연구 포인트 추가와 사용
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

    //도구
    public bool isUseAxe { get; private set; } // 도끼 사용 가능
    public bool isUsePickax { get; private set; } // 곡괭이 사용 가능
    public bool isUseStrongPickax { get; private set; } // 강한 곡괭이 사용 가능

    //농사
    public bool farmAble { get; private set; } // 땅 설치시 확인
    public float farmValue { get; private set; } // 땅에서 얻을 시 확인

    //건설
    public bool isBuildingWooden { get; private set; } // 나무 건물 건설 가능
    public bool isBuildingIron { get; private set; } // 철 건물 건설 가능
    public bool isFixingBoat { get; private set; } // 보트 고치는것 가능

    //전기
    public bool isBuildingGenerator { get; private set; } // 발전기 건설 가능
    public bool isbuildingLighthouse { get; private set; } // 등대 건설 가능
    public bool isFixingElectricity { get; private set; } // 전기 고치기 가능

    public void ToolLevel1Upgrade() //도구 레벨1 업그레이드
    {
        isUseAxe = true;
    }

    public void ToolLevel2Upgrade() //도구 레벨2 업그레이드
    {
        isUsePickax = true;
    }

    public void ToolLevel3Upgrade() //도구 레벨3 업그레이드
    {
        isUseStrongPickax = true;
    }

    public void FarmLevel1Upgrade() //농사 레벨1 업그레이드
    {
        farmAble = true;
        farmValue = 1f;
    }

    public void FarmLevel2Upgrade() //농사 레벨2 업그레이드
    {
        if (farmAble)
        {
            farmValue = 1.5f;
        }
    }

    public void FarmLevel3Upgrade() //농사 레벨3 업그레이드
    {
        if (farmAble)
        {
            farmValue = 2f;
        }
    }

    public void BuildLevel1Upgrade() //건설 레벨1 업그레이드
    {
        isBuildingWooden = true;
    }

    public void BuildLevel2Upgrade() //건설 레벨2 업그레이드
    {
        isBuildingIron = true;
    }

    public void BuildLevel3Upgrade() //건설 레벨3 업그레이드
    {
        isFixingBoat = true;
    }

    public void ElectricityLevel1Upgrade() //전기 레벨1 업그레이드
    {
        isBuildingGenerator = true;
    }

    public void ElectricityLevel2Upgrade() //전기 레벨2 업그레이드
    {
        isbuildingLighthouse = true;
    }

    public void ElectricityLevel3Upgrade() //전기 레벨3 업그레이드
    {
        isFixingElectricity = true;
    }
}
