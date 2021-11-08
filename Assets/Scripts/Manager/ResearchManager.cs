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

    public bool farmAble { get; private set; } // �� ��ġ�� Ȯ��
    public float farmValue { get; private set; } // ������ ���� �� Ȯ��
    public bool level3 { get; private set; } // TODO: Level3 ��� �ֱ�

    public bool stoneAble { get; private set; } // �� Ķ�� Ȯ��
    public bool ironAble { get; private set; } // ö Ķ�� Ȯ��
    public bool mineLevel3 { get; private set; } // TODO: Level3 ��� �ֱ�

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
