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
            if (!ResearchManager.Instance.isBuildingIron // ��� �˽�
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
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
            if (!ResearchManager.Instance.isbuildingLighthouse
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.Electricity] < 20)
            {
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
            if (!ResearchManager.Instance.isBuildingWooden || !ResearchManager.Instance.farmAble || !InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() => // �ǹ� 3�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingGenerator
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
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
            if (!ResearchManager.Instance.isBuildingWooden || !InventoryManager.UseItem(InventoryItem.Tree, 10))
            {
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[4]);
            GameManager.Instance.isBoat = true;
        });

        buildButton[5].onClick.AddListener(() => // �ǹ� 5�� ��ġ
        {
            if (!ResearchManager.Instance.isBuildingWooden 
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1)
            {
                return;
            }

            Debug.Log("��ġ");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);

            GameManager.Instance.buildObject = Instantiate(buildObject[5]);
        });
    }
}
