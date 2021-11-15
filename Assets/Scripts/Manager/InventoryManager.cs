using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : Singleton<InventoryManager>
{
    public Item[] items;

    public static int GetItemCount(InventoryItem item)
    {
        return Instance.items[(int)item].count;
    }

    public static void AddItem(InventoryItem item, int count)
    {
        Instance.items[(int)item].count += count;
        Instance.items[(int)item].countText.text = Instance.items[(int)item].count.ToString();
    }

    public static bool UseItem(InventoryItem item, int count)
    {
        if (Instance.items[(int)item].count >= count)
        {
            Instance.items[(int)item].count -= count;
            Instance.items[(int)item].countText.text = Instance.items[(int)item].count.ToString();

            return true;
        }

        return false;
    }
}
