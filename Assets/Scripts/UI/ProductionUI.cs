using UnityEngine;
using UnityEngine.UI;

public class ProductionUI : ChangeUI // 제작 관련된 UI를 모아둔 클래스
{
    [SerializeField] private Button productionButton; // 제작 버튼

    [SerializeField] private Button[] productionButtons; // 제작할 아이템을 선택하는 버튼
    [SerializeField] private Panel[] needItemPanel; // 필요한 아이템을 보여줌

    [SerializeField] private NeedItem[] needItems; // 필요한 아이템
    [SerializeField] private MakeItem[] makeItems; // 만들 수 있는 아이템

    private int currentProductionNumber = -1; // 현재 선택하고 있는 제작 아이디

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < productionButtons.Length; i++) // 모든 버튼에 함수를 넣음
        {
            Panel panel = needItemPanel[needItems[i].needItems.Length - 1];
            int num = i;

            productionButtons[num].onClick.AddListener(() => // 제작할 아이템을 선택하면
            {
                if (currentProductionNumber >= 0) // 제작할 아이템 확인
                {
                    needItemPanel[needItems[currentProductionNumber].needItems.Length - 1].gameObject.SetActive(false); //원래 보여주던거 지움
                }

                for (int j = 0; j < needItems[num].needItems.Length; j++) // 아이템 UI에 정보를 집어넣음
                {
                    panel.images[j].sprite = needItems[num].needItemSprites[j];
                    panel.texts[j].text = needItems[num].count[j].ToString();
                }

                panel.gameObject.SetActive(true); // 만든 UI를 보여줌
                currentProductionNumber = num;
            });
        }

        productionButton.onClick.AddListener(() => //제작 버튼을 누르면
        {
            if (currentProductionNumber < 0)
            {
                return;
            }

            NeedItem needItem = needItems[currentProductionNumber]; //필요한 아이템을 불러옴
            MakeItem makeItem = makeItems[currentProductionNumber]; //만들 수 있는 아이템을 불러옴

            for (int i = 0; i < needItem.needItems.Length; i++) //필요한 아이템이 모두 있는지 확인
            {
                if (InventoryManager.GetItemCount(needItem.needItems[i]) < needItem.count[i])
                {
                    return;
                }
            }

            for (int i = 0; i < needItem.needItems.Length; i++) //필요한 아이템을 모두 사용
            {
                InventoryManager.UseItem(needItem.needItems[i], needItem.count[i]);
            }

            ResearchManager.Instance.ReserchPoint += 1; //연구포인트 업그레이드

            switch (makeItem.makeItemType) //만든 아이템 종류에 따라 다른 방법으로 아이템을 더해줌
            {
                case MakeItemType.Inventory:
                    InventoryManager.AddItem(makeItem.makeItem, makeItem.count);
                    break;
                case MakeItemType.TopUI:
                    GameManager.topUICount[makeItem.makeTopUIItem] += makeItem.count;
                    break;
                case MakeItemType.Electrical:
                    ProductionManager.Instance.GetElectricalItem();
                    break;
            }
        });
    }
}
