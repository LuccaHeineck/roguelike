using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem itemPrefab;
    [SerializeField] private RectTransform contentPanel;
    public ItemDescription itemDescription;
    List<InventoryItem> listOfItems = new List<InventoryItem>();

    public void ShowInventory() => gameObject.SetActive(true);
    public void HideInventory() => gameObject.SetActive(false);
    public bool IsVisible() => gameObject.activeSelf;

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

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
        listOfItems[0].SetData(tempSprite, tempQuantity);
    }

    private void HandleItemSelection(InventoryItem item)
    {
        // Sprite sprite = item.GetComponent<Image>().sprite;
        // string title = item.title;
        // string description = item.description;
        // string effects = item.effects;
        //itemDescription.SetDescription(sprite, title, description, effects);
        itemDescription.SetDescription(tempSprite, tempTitle, tempDescription, tempEffects);
        listOfItems[0].activateSelector();
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

    public Sprite tempSprite;
    public int tempQuantity;
    public string tempTitle, tempDescription, tempEffects;

    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfItems[0].SetData(tempSprite, tempQuantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
