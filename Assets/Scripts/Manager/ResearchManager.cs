using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : Singleton<ResearchManager>
{
    [SerializeField] private ResearchUI researchUI = null;

    private int researchPoint = 0;

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

    //����
    public bool isUseAxe { get; private set; } // ���� ��� ����
    public bool isUsePickax { get; private set; } // ��� ��� ����
    public bool isUseStrongPickax { get; private set; } // ���� ��� ��� ����

    //���
    public bool farmAble { get; private set; } // �� ��ġ�� Ȯ��
    public float farmValue { get; private set; } // ������ ���� �� Ȯ��

    //�Ǽ�
    public bool isBuildingWooden { get; private set; } // ���� �ǹ� �Ǽ� ����
    public bool isBuildingIron { get; private set; } // ö �ǹ� �Ǽ� ����
    public bool isFixingBoat { get; private set; } // ��Ʈ ��ġ�°� ����

    //����
    public bool isBuildingGenerator { get; private set; } // ������ �Ǽ� ����
    public bool isbuildingLighthouse { get; private set; } // ��� �Ǽ� ����
    public bool isFixingElectricity { get; private set; } // ���� ��ġ�� ����

    public void ToolLevel1Upgrade()
    {
        isUseAxe = true;
    }

    public void ToolLevel2Upgrade()
    {
        isUsePickax = true;
    }

    public void ToolLevel3Upgrade()
    {
        isUseStrongPickax = true;
    }

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
        if (farmAble)
        {
            farmValue = 2f;
        }
    }

    public void BuildLevel1Upgrade()
    {
        isBuildingWooden = true;
    }

    public void BuildLevel2Upgrade()
    {
        isBuildingIron = true;
    }

    public void BuildLevel3Upgrade()
    {
        isFixingBoat = true;
    }

    public void ElectricityLevel1Upgrade()
    {
        isBuildingGenerator = true;
    }

    public void ElectricityLevel2Upgrade()
    {
        isbuildingLighthouse = true;
    }

    public void ElectricityLevel3Upgrade()
    {
        isFixingElectricity = true;
    }
}
