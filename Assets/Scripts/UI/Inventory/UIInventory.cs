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

    public void InitializeUIInventory(int maxSlots)
    {
        foreach (var item in listOfItems) Destroy(item.gameObject);
        listOfItems.Clear();

        int numberOfItems = UIInvControl.pInventory.Stacks.Count;

        for (int i = 0; i < maxSlots; i++)
        {
            UIInventoryItem uiItem = Instantiate(UIItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(UIInventoryGrid);

            if (i < numberOfItems)
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


    public void Show()
    {
        gameObject.SetActive(true);
        UIDescription.ResetDescription();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

}
