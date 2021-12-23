using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string itemName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.Instance.Show(itemName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.Hide();
    }
}
