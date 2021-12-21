public class InventoryManager : Singleton<InventoryManager>
{
    public Item[] items; // ���� ������ �ִ� ������
    public InventoryUI inventoryUI; // �κ��丮 UI

    private void Start()
    {
        if (GameManager.Instance.gameData.isStart)
        {
            for (int i = 0; i < items.Length; i++)
            {
                AddItem(i, GameManager.Instance.gameData.items[i].count);
            }
        }
    }

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

    private static void AddItem(int num, int count)
    {
        Instance.items[num].count += count;
        Instance.inventoryUI.SetText(Instance.items[num]);
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
