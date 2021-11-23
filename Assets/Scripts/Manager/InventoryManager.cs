public class InventoryManager : Singleton<InventoryManager>
{
    public Item[] items; // 현재 가지고 있는 아이템
    public InventoryUI inventoryUI; // 인벤토리 UI

    /// <summary>
    /// 현재 아이템을 가지고 있는 갯수를 출력
    /// </summary>
    /// <param name="item">인벤토리 아이템</param>
    /// <returns></returns>
    public static int GetItemCount(InventoryItem item)
    {
        return Instance.items[(int)item].count;
    }

    /// <summary>
    /// 갯수만큼 인벤토리 아이템을 늘림
    /// </summary>
    /// <param name="item">인벤토리 아이템</param>
    /// <param name="count">갯수</param>
    public static void AddItem(InventoryItem item, int count)
    {
        Instance.items[(int)item].count += count;
        Instance.inventoryUI.SetText(Instance.items[(int)item]);
    }

    /// <summary>
    /// 갯수만큼 인벤토리 아이템이 있는지 확인하고 있다면 갯수만큼 빼주고 true 반환, 없다면 false 반환
    /// </summary>
    /// <param name="item">인벤토리 아이템</param>
    /// <param name="count">갯수</param>
    /// <returns></returns>
    public static bool UseItem(InventoryItem item, int count)
    {
        if (Instance.items[(int)item].count >= count)
        {
            Instance.items[(int)item].count -= count;
            Instance.inventoryUI.SetText(Instance.items[(int)item]);

            return true;
        }

        return false;
    }
}
