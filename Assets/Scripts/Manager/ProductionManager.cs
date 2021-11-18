using UnityEngine;
using UnityEngine.UI;

public class ProductionManager : MonoBehaviour
{
    [SerializeField] private Text explanationText;
    [SerializeField] private Button productionButton;

    [SerializeField] private Button[] productionButtons;
    [SerializeField] private string[] productionMessage;

    private int currentProductionNumber = -1;

    private NeedItem[] needItems;
    private MakeItem[] makeItems;

    [SerializeField] private Vector2Int limitElectricalPartsCount;
    [SerializeField] private Vector2Int limitElectricWiresCount;

    private void Start()
    {
        for (int i = 0; i < productionButtons.Length; i++)
        {
            productionButtons[i].onClick.AddListener(() =>
            {
                explanationText.text = productionMessage[i];

                currentProductionNumber = i;
            });
        }

        productionButton.onClick.AddListener(() =>
        {
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

            switch (makeItem.makeItemType)
            {
                case MakeItemType.Inventory:
                    InventoryManager.AddItem(makeItem.makeItem, makeItem.count);
                    break;
                case MakeItemType.TopUI:
                    GameManager.topUICount[makeItem.makeTopUIItem] += makeItem.count;
                    break;
                case MakeItemType.Electrical:
                    GetElectricalItem();
                    break;
            }
        });
    }

    private void GetElectricalItem()
    {
        int electricalPartsCount = Random.Range(limitElectricalPartsCount.x, limitElectricalPartsCount.y);
        int electricWiresCount = Random.Range(limitElectricWiresCount.x, limitElectricWiresCount.y);

        InventoryManager.AddItem(InventoryItem.ElectricalParts, electricalPartsCount);
        InventoryManager.AddItem(InventoryItem.ElectricWires, electricWiresCount);
    }
}