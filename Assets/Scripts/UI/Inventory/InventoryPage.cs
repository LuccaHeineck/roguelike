using System.Collections.Generic;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField] private InventoryItem UIItemPrefab;
    [SerializeField] private RectTransform UIContentPanel;
    public InventoryDescription UIDescriptionPanel;
    List<InventoryItem> UIlistOfItems = new List<InventoryItem>();

    public void ShowInventory() => gameObject.SetActive(true);
    public void HideInventory() => gameObject.SetActive(false);
    public bool IsVisible() => gameObject.activeSelf;

    private void Awake()
    {
        Hide();
        UIDescriptionPanel.ResetDescription();
    }

    public void initializeInventoryUI(int numberOfItems)
    {
        for (int i = 0; i < numberOfItems; i++)
        {
            InventoryItem uiItem = Instantiate(UIItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(UIContentPanel);
            UIlistOfItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemRightClicked += HandleShowItemActions;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
        }

        HideInventory();
        UIlistOfItems[0].SetData(tempSprite, tempQuantity);
    }

    private void HandleItemSelection(InventoryItem uiItem)
    {
        // Sprite sprite = uiItem.GetComponent<Image>().sprite;
        // string title = uiItem.title;
        // string description = uiItem.description;
        // string effects = uiItem.effects;
        //UIDescriptionPanel.SetDescription(sprite, title, description, effects);
        UIDescriptionPanel.SetDescription(tempSprite, tempTitle, tempDescription, tempEffects);
        UIlistOfItems[0].activateSelector();
    }
    private void HandleShowItemActions(InventoryItem uiItem)
    {

    }

    private void HandleBeginDrag(InventoryItem uiItem)
    {

    }

    private void HandleEndDrag(InventoryItem uiItem)
    {


    }

    private void HandleSwap(InventoryItem uiItem)
    {

    }

    public Sprite tempSprite;
    public int tempQuantity;
    public string tempTitle, tempDescription, tempEffects;

    public void Show()
    {
        gameObject.SetActive(true);
        UIDescriptionPanel.ResetDescription();

        UIlistOfItems[0].SetData(tempSprite, tempQuantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
