public class InventoryUI : ChangeUI
{
    protected override void Start()
    {
        base.Start();

        InventoryManager.Instance.inventoryUI = this;
    }

    public void SetText(Item item)
    {
        item.countText.text = item.count.ToString();
    }
}