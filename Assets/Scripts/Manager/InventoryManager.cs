public class InventoryManager : Singleton<InventoryManager>
{
    public Item[] items; // ���� ������ �ִ� ������
    public InventoryUI inventoryUI; // �κ��丮 UI

    /// <summary>
    /// ���� �������� ������ �ִ� ������ ���
    /// </summary>
    /// <param name="item">�κ��丮 ������</param>
    /// <returns></returns>
    public static int GetItemCount(InventoryItem item)
    {
        return Instance.items[(int)item].count;
    }

    /// <summary>
    /// ������ŭ �κ��丮 �������� �ø�
    /// </summary>
    /// <param name="item">�κ��丮 ������</param>
    /// <param name="count">����</param>
    public static void AddItem(InventoryItem item, int count)
    {
        Instance.items[(int)item].count += count;
        Instance.inventoryUI.SetText(Instance.items[(int)item]);
    }

    /// <summary>
    /// ������ŭ �κ��丮 �������� �ִ��� Ȯ���ϰ� �ִٸ� ������ŭ ���ְ� true ��ȯ, ���ٸ� false ��ȯ
    /// </summary>
    /// <param name="item">�κ��丮 ������</param>
    /// <param name="count">����</param>
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
