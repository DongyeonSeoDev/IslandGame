using UnityEngine;

public class ProductionManager : Singleton<ProductionManager> //���� ���� Ŭ����
{
    [SerializeField] private Vector2Int limitElectricalPartsCount; // �����ǰ ���� ���� x�� �ּ�, y�� �ִ�
    [SerializeField] private Vector2Int limitElectricWiresCount; // ���⼱ ���� ���� x�� �ּ�, y�� �ִ�

    public void GetElectricalItem() // �����ǰ�� �����´�
    {
        //���� ������ ������
        int electricalPartsCount = Random.Range(limitElectricalPartsCount.x, limitElectricalPartsCount.y);
        int electricWiresCount = Random.Range(limitElectricWiresCount.x, limitElectricWiresCount.y);

        //�κ��丮�� �߰�
        InventoryManager.AddItem(InventoryItem.ElectricalParts, electricalPartsCount);
        InventoryManager.AddItem(InventoryItem.ElectricWires, electricWiresCount);
    }
}