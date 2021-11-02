using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup inventoryCanvasGroup;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Image fill;
    [SerializeField] private Text text;
    [SerializeField] private float speed = 0.5f;

    private bool isShow = false;

    private int currentInventory = 0;
    private int maxInventory = 30;

    public int CurrentInventory
    {
        get
        {
            return currentInventory;
        }

        set
        {
            if (value <= maxInventory)
            {
                currentInventory = value;
                ChangeItemCount(currentInventory);
            }
        }
    }

    private void Start()
    {
        inventoryButton.onClick.AddListener(() =>
        {
            isShow = !isShow;

            if (isShow)
            {
                inventoryCanvasGroup.interactable = true;
                inventoryCanvasGroup.blocksRaycasts = true;
            }

            UIManager.ChangeUI(inventoryCanvasGroup, speed, isShow, () =>
            {
                if (!isShow)
                {
                    inventoryCanvasGroup.interactable = false;
                    inventoryCanvasGroup.blocksRaycasts = false;
                }
            });
        });
    }

    public void ChangeItemCount(int count)
    {
        text.text = $"{count.ToString("00")} / {maxInventory}";
        fill.fillAmount = (float)count / maxInventory;
    }
}
