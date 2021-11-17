public class InventoryManager : Singleton<InventoryManager>
{
    public Item[] items;
    public InventoryUI inventoryUI;

    public static int GetItemCount(InventoryItem item)
    {
        return Instance.items[(int)item].count;
    }

    public static void AddItem(InventoryItem item, int count)
    {
        Instance.items[(int)item].count += count;
        Instance.inventoryUI.SetText(Instance.items[(int)item]);
    }

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
