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
            if (!ResearchManager.Instance.isBuildingIron)
            {
                Tooltip.Instance.Show("철 건물 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10 // 재료 검사
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
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
            if (!ResearchManager.Instance.isbuildingLighthouse)
            {
                Tooltip.Instance.Show("등대 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1
            || GameManager.topUICount[TopUI.Electricity] < 20)
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
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
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("나무 건물 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (!ResearchManager.Instance.farmAble || !InventoryManager.UseItem(InventoryItem.Tree, 5))
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[2]);
        });

        buildButton[3].onClick.AddListener(() => // 건물 3번 설치
        {
            if (!ResearchManager.Instance.isBuildingGenerator)
            {
                Tooltip.Instance.Show("발전기 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 2)
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
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
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("나무 건물 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (!InventoryManager.UseItem(InventoryItem.Tree, 10))
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            GameManager.Instance.buildObject = Instantiate(buildObject[4]);
            GameManager.Instance.isBoat = true;
        });

        buildButton[5].onClick.AddListener(() => // 건물 5번 설치
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("나무 건물 건설 연구가 되어있지 않습니다.", 1f);
                return;
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 15
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 5
            || InventoryManager.GetItemCount(InventoryItem.Iron) < 1)
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 15);
            InventoryManager.UseItem(InventoryItem.Stone, 5);
            InventoryManager.UseItem(InventoryItem.Iron, 1);

            GameManager.Instance.buildObject = Instantiate(buildObject[5]);
        });

        buildButton[6].onClick.AddListener(() => // 건물 6번 설치
        {
            if (!ResearchManager.Instance.isBuildingWooden)
            {
                Tooltip.Instance.Show("나무 건물 건설 연구가 되어있지 않습니다.", 1f);
            }

            if (InventoryManager.GetItemCount(InventoryItem.Tree) < 10
            || InventoryManager.GetItemCount(InventoryItem.Stone) < 10)
            {
                Tooltip.Instance.Show("재료가 부족합니다.", 1f);
                return;
            }

            Debug.Log("설치");
            ResearchManager.Instance.ReserchPoint += 1;

            InventoryManager.UseItem(InventoryItem.Tree, 10);
            InventoryManager.UseItem(InventoryItem.Stone, 10);

            GameManager.Instance.buildObject = Instantiate(buildObject[6]);
        });
    }
}
