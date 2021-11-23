using UnityEngine;

[System.Serializable]
public class NeedItem // 조합에 필요한 아이템 관리 클래스
{
    public InventoryItem[] needItems;
    public Sprite[] needItemSprites;
    public int[] count;
}
