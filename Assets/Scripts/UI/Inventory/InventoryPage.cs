using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public void ShowInventory() => gameObject.SetActive(true);
    public void HideInventory() => gameObject.SetActive(false);
    public bool IsVisible() => gameObject.activeSelf;

    public void initializeInventoryUI(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            listOfItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemRightClicked += HandleShowItemActions;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
        }

        HideInventory();
    }
    private void HandleItemSelection(InventoryItem item)
    {

    }

    private void HandleShowItemActions(InventoryItem item)
    {

    }

    private void HandleBeginDrag(InventoryItem item)
    {

    }

    private void HandleEndDrag(InventoryItem item)
    {


    }

    private void HandleSwap(InventoryItem item)
    {

    }

}
