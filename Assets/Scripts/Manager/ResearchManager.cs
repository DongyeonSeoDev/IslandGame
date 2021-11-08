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

    public bool farmable { get; private set; } // �� ��ġ�� Ȯ��
    public float farmValue { get; private set; } // ������ ���� �� Ȯ��
    public bool level3 { get; private set; } // TODO: Level3 ��� �ֱ�

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