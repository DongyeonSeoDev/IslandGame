using UnityEngine;
using UnityEngine.UI;

public class BuildUI : ChangeUI // 건설과 관련된 UI를 여기서 관리함
{
    [SerializeField] private Button[] buildButton = null; // 건설 버튼
    [SerializeField] private GameObject[] buildObject = null; // 건설 오브젝트

    protected override void Start()
    {
        base.Start();

        buildButton[0].onClick.AddListener(() => // 건물 0번 설치
        {
            if (!ResearchManager.Instance.isBuildingIron // 재료 검시
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1; // 연구포인트 업

            InventoryManager.UseItem(InventoryItem.Tree, 10); // 재료 사용
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 2);

            GameManager.Instance.buildObject = Instantiate(buildObject[0]); // 건물 설치
        });

        buildButton[1].onClick.AddListener(() => // 건물 1번 설치
        {
            if (!ResearchManager.Instance.isbuildingLighthouse
            || InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.Electricity] < 20)
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);
            GameManager.topUICount[TopUI.Electricity] -= 20;

            GameManager.Instance.buildObject = Instantiate(buildObject[1]);
        });

        buildButton[2].onClick.AddListener(() => // 건물 2번 설치
        {
            if (!ResearchManager.Instance.isBuildingWooden || !ResearchManager.Instance.farmAble || !InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() => // 건물 3번 설치
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

        buildButton[4].onClick.AddListener(() => // 건물 4번 설치
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

        buildButton[5].onClick.AddListener(() => // 건물 5번 설치
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
