using UnityEngine;
using UnityEngine.UI;

public class BuildUI : ChangeUI
{
    [SerializeField] private Button[] buildButton = null;
    [SerializeField] private GameObject[] buildObject = null;

    protected override void Start()
    {
        base.Start();

        buildButton[0].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isBuildingIron
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            GameManager.Instance.buildObject = Instantiate(buildObject[0]);
        });

        buildButton[1].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isbuildingLighthouse
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.electricity] < 20)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);
            GameManager.topUICount[TopUI.electricity] -= 20;

            GameManager.Instance.buildObject = Instantiate(buildObject[1]);
        });

        buildButton[2].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isBuildingWooden || !ResearchManager.Instance.farmAble || !InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isBuildingGenerator
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            GameManager.Instance.buildObject = Instantiate(buildObject[3]);
            GameManager.Instance.isSunPower = true;
        });

        buildButton[4].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isBuildingWooden || !InventoryManager.UseItem(InventoryItem.Tree, 10))
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[4]);
            GameManager.Instance.isBoat = true;
        });

        buildButton[5].onClick.AddListener(() =>
        {
            if (!ResearchManager.Instance.isBuildingWooden 
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);

            GameManager.Instance.buildObject = Instantiate(buildObject[5]);
        });
    }
}
