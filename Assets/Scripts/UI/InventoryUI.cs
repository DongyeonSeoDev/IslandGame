public class InventoryUI : ChangeUI // �κ��丮 UI�� �����ϴ� Ŭ����
{
    protected override void Start()
    {
        base.Start();

        InventoryManager.Instance.inventoryUI = this;
    }

    public void SetText(Item item) // �κ��丮 �ؽ�Ʈ ����
    {
        item.countText.text = item.count.ToString();
    }
}