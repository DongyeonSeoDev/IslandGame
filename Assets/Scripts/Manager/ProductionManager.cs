using UnityEngine;
using UnityEngine.UI;

public class ProductionManager : Singleton<ProductionManager>
{
    [SerializeField] private Vector2Int limitElectricalPartsCount;
    [SerializeField] private Vector2Int limitElectricWiresCount;

    public void GetElectricalItem()
    {
        int electricalPartsCount = Random.Range(limitElectricalPartsCount.x, limitElectricalPartsCount.y);
        int electricWiresCount = Random.Range(limitElectricWiresCount.x, limitElectricWiresCount.y);

        InventoryManager.AddItem(InventoryItem.ElectricalParts, electricalPartsCount);
        InventoryManager.AddItem(InventoryItem.ElectricWires, electricWiresCount);
    }
}