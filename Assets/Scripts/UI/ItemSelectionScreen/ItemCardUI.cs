// ItemCardUI.cs
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ItemCardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image iconImage;
    [SerializeField] private Button button;

    private ItemData currentItem;

    public void Setup(ItemData itemData, Action<ItemData> onSelected)
    {
        currentItem = itemData;

        itemNameText.text = itemData.ItemName;        // era itemData.itemName
        descriptionText.text = itemData.ItemDescription; // era itemData.description

        if (itemData.ItemSprite != null)
            iconImage.sprite = itemData.ItemSprite;      // era itemData.sprite

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onSelected(currentItem));
    }
}