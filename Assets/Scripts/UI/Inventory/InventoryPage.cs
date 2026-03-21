using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    List<InventoryItem> listOfItems = new List<InventoryItem>();


    public void initializeInventoryUI(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
            initializeItemUI();

        HideInventory();
    }



    public void initializeItemUI()
    {
        InventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        uiItem.transform.SetParent(contentPanel);
        listOfItems.Add(uiItem);
    }

    public void ShowInventory() => gameObject.SetActive(true);
    public void HideInventory() => gameObject.SetActive(false);
    public bool IsVisible() => gameObject.activeSelf;

}
