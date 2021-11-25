using UnityEngine;

public class ProductionManager : Singleton<ProductionManager> //제작 관련 클래스
{
    [SerializeField] private Vector2Int limitElectricalPartsCount; // 전기부품 갯수 제한 x가 최소, y가 최대
    [SerializeField] private Vector2Int limitElectricWiresCount; // 전기선 갯수 제한 x가 최소, y가 최대

    public void GetElectricalItem() // 전기부품을 가져온다
    {
        //랜덤 갯수를 가져옴
        int electricalPartsCount = Random.Range(limitElectricalPartsCount.x, limitElectricalPartsCount.y);
        int electricWiresCount = Random.Range(limitElectricWiresCount.x, limitElectricWiresCount.y);

        //인벤토리에 추가
        InventoryManager.AddItem(InventoryItem.ElectricalParts, electricalPartsCount);
        InventoryManager.AddItem(InventoryItem.ElectricWires, electricWiresCount);
    }
}