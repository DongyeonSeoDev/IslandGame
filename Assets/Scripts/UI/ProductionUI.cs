using UnityEngine;
using UnityEngine.UI;

public class ProductionUI : ChangeUI
{
    [SerializeField] private Button productionButton;

    [SerializeField] private Button[] productionButtons;
    [SerializeField] private Panel[] needItemPanel;

    [SerializeField] private NeedItem[] needItems;
    [SerializeField] private MakeItem[] makeItems;

    private int currentProductionNumber = -1;

    protected override void Start()
    {
        base.Start();

        for (int i = 0; i < productionButtons.Length; i++)
        {
            Panel panel = needItemPanel[needItems[i].needItems.Length - 1];
            int num = i;

            productionButtons[num].onClick.AddListener(() =>
            {
                if (currentProductionNumber >= 0)
                {
                    needItemPanel[needItems[currentProductionNumber].needItems.Length - 1].gameObject.SetActive(false);
                }

                for (int j = 0; j < needItems[num].needItems.Length; j++)
                {
                    panel.images[j].sprite = needItems[num].needItemSprites[j];
                    panel.texts[j].text = needItems[num].count[j].ToString();
                }

                panel.gameObject.SetActive(true);
                currentProductionNumber = num;
            });
        }

        productionButton.onClick.AddListener(() =>
        {
            if (currentProductionNumber < 0)
            {
                return;
            }

            NeedItem needItem = needItems[currentProductionNumber];
            MakeItem makeItem = makeItems[currentProductionNumber];

            for (int i = 0; i < needItem.needItems.Length; i++)
            {
                if (InventoryManager.GetItemCount(needItem.needItems[i]) < needItem.count[i])
                {
                    return;
                }
            }

            for (int i = 0; i < needItem.needItems.Length; i++)
            {
                InventoryManager.UseItem(needItem.needItems[i], needItem.count[i]);
            }

            ResearchManager.Instance.ReserchPoint += 1;

            switch (makeItem.makeItemType)
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
