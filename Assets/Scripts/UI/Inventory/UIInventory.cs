using System.Collections.Generic;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIInventoryItem UIItemPrefab;
    [SerializeField] private RectTransform UIInventoryGrid;

    UIInventoryControl UIInvControl;
    public UIDescription UIDescription;
    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    private void Awake()
    {

        Hide();
        UIDescription.ResetDescription();
    }

    public void setInventoryControl(UIInventoryControl UIInvControl) => this.UIInvControl = UIInvControl;

    public void InitializeUIInventory(int numberOfItems)
    {
        foreach (var item in listOfItems) Destroy(item.gameObject);
        listOfItems.Clear();

        for (int i = 0; i < numberOfItems; i++)
        {
            UIInventoryItem uiItem = Instantiate(UIItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(UIInventoryGrid);

            if (UIInvControl.pInventory.Stacks[i] != null)
            {
                uiItem.ConvertItemIntoUIItem(UIInvControl.pInventory.Stacks[i]);
                listOfItems.Add(uiItem);
            }

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemRightClicked += HandleShowItemActions;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
        }

        HideInventory();
    }

    public void ShowInventory() => gameObject.SetActive(true);
    public void HideInventory() => gameObject.SetActive(false);
    public bool IsVisible() => gameObject.activeSelf;

    private void HandleItemSelection(UIInventoryItem item)
    {
        // Sprite sprite = item.GetComponent<Image>().sprite;
        // string title = item.title;
        // string description = item.description;
        // string effects = item.effects;
        //UIDescription.SetDescription(sprite, title, description, effects);
        UIDescription.SetDescription(tempSprite, tempTitle, tempDescription, tempEffects);
        listOfItems[0].activateSelector();
    }
    private void HandleShowItemActions(UIInventoryItem item)
    {

    }

    private void HandleBeginDrag(UIInventoryItem item)
    {

    }

    private void HandleEndDrag(UIInventoryItem item)
    {


    }

    private void HandleSwap(UIInventoryItem item)
    {

    }

    public Sprite tempSprite;
    public int tempQuantity;
    public string tempTitle, tempDescription, tempEffects;

    public void Show()
    {
        gameObject.SetActive(true);
        UIDescription.ResetDescription();

        listOfItems[0].SetData(tempSprite, tempQuantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
