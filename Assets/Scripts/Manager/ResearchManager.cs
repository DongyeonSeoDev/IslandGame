using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResearchManager : Singleton<ResearchManager>
{
    [SerializeField] private ResearchUI researchUI = null; //���� UI

    private int researchPoint = 0; //���� ����Ʈ

    //���� ����Ʈ �߰��� ���
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

    public void ToolLevel1Upgrade() //���� ����1 ���׷��̵�
    {
        isUseAxe = true;
    }

    public void ToolLevel2Upgrade() //���� ����2 ���׷��̵�
    {
        isUsePickax = true;
    }

    public void ToolLevel3Upgrade() //���� ����3 ���׷��̵�
    {
        isUseStrongPickax = true;
    }

    public void FarmLevel1Upgrade() //��� ����1 ���׷��̵�
    {
        farmAble = true;
        farmValue = 1f;
    }

    public void FarmLevel2Upgrade() //��� ����2 ���׷��̵�
    {
        if (farmAble)
        {
            farmValue = 1.5f;
        }
    }

    public void FarmLevel3Upgrade() //��� ����3 ���׷��̵�
    {
        if (farmAble)
        {
            farmValue = 2f;
        }
    }

    public void BuildLevel1Upgrade() //�Ǽ� ����1 ���׷��̵�
    {
        isBuildingWooden = true;
    }

    public void BuildLevel2Upgrade() //�Ǽ� ����2 ���׷��̵�
    {
        isBuildingIron = true;
    }

    public void BuildLevel3Upgrade() //�Ǽ� ����3 ���׷��̵�
    {
        isFixingBoat = true;
    }

    public void ElectricityLevel1Upgrade() //���� ����1 ���׷��̵�
    {
        isBuildingGenerator = true;
    }

    public void ElectricityLevel2Upgrade() //���� ����2 ���׷��̵�
    {
        isbuildingLighthouse = true;
    }

    public void ElectricityLevel3Upgrade() //���� ����3 ���׷��̵�
    {
        isFixingElectricity = true;
    }
}
