public class InventoryUI : ChangeUI // 인벤토리 UI를 관리하는 클래스
{
    protected override void Start()
    {
        base.Start();

        InventoryManager.Instance.inventoryUI = this;
    }

    public void SetText(Item item) // 인벤토리 텍스트 설정
    {
        item.countText.text = item.count.ToString();
    }
}