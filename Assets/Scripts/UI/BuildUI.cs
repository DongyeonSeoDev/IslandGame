using UnityEngine;
using UnityEngine.UI;

public class BuildUI : ChangeUI // �Ǽ��� ���õ� UI�� ���⼭ ������
{
    [SerializeField] private Button[] buildButton = null; // �Ǽ� ��ư
    [SerializeField] private GameObject[] buildObject = null; // �Ǽ� ������Ʈ

    protected override void Start()
    {
        base.Start();

        buildButton[0].onClick.AddListener(() => // �ǹ� 0�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingIron)
            {
                Tooltip.Instance.Show("ö �ǹ� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10 // ��� �˻�
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1; // ��������Ʈ ��

            InventoryManager.UseItem(InventoryItem.Tree, 10); // ��� ���
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            GameManager.Instance.buildObject = Instantiate(buildObject[0]); // �ǹ� ��ġ
        });

        buildButton[1].onClick.AddListener(() => // �ǹ� 1�� ��ġ
        {
            if (!ResearchManager.Instance.isbuildingLighthouse)
            {
                Tooltip.Instance.Show("��� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.Electricity] < 20)
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);
            GameManager.topUICount[TopUI.Electricity] -= 20;

            GameManager.Instance.buildObject = Instantiate(buildObject[1]);
        });

        buildButton[2].onClick.AddListener(() => // �ǹ� 2�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("���� �ǹ� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (!ResearchManager.Instance.farmAble || !InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() => // �ǹ� 3�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingGenerator)
            {
                Tooltip.Instance.Show("������ �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            GameManager.Instance.buildObject = Instantiate(buildObject[3]);
            GameManager.Instance.isSunPower = true;
        });

        buildButton[4].onClick.AddListener(() => // �ǹ� 4�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("���� �ǹ� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (!InventoryManager.UseItem(InventoryItem.Tree, 10))
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[4]);
            GameManager.Instance.isBoat = true;
        });

        buildButton[5].onClick.AddListener(() => // �ǹ� 5�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("���� �ǹ� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1)
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);

            GameManager.Instance.buildObject = Instantiate(buildObject[5]);
        });

        buildButton[6].onClick.AddListener(() => // �ǹ� 6�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("���� �ǹ� �Ǽ� ������ �Ǿ����� �ʽ��ϴ�.", 1f);
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 10)
            {
                Tooltip.Instance.Show("��ᰡ �����մϴ�.", 1f);
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 10);

            GameManager.Instance.buildObject = Instantiate(buildObject[6]);
        });
    }
}
