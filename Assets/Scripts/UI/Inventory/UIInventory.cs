using System.Collections.Generic;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIInventoryItem UIItemPrefab;
    [SerializeField] private RectTransform UIInventoryGrid;
    public UIDescription UIDescription;

    UIInventoryControl UIInvControl;
    List<UIInventoryItem> listOfItems = new List<UIInventoryItem>();

    int lastItemSelected = 0;

    private void Awake()
    {
        HideInventory();
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
            // uiItem.OnItemBeginDrag += HandleBeginDrag;
            // uiItem.OnItemEndDrag += HandleEndDrag;
            // uiItem.OnItemDroppedOn += HandleSwap;
        }

        HideInventory();
    }

    public bool IsVisible() => gameObject.activeSelf;

    public void ShowInventory()
    {
        gameObject.SetActive(true);
        UIDescription.ResetDescription();
    }

    public void HideInventory() => gameObject.SetActive(false);


    private void HandleItemSelection(UIInventoryItem item)
    {
        listOfItems[lastItemSelected].desactivateSelector();
        lastItemSelected = item.transform.GetSiblingIndex();
        item.activateSelector();

        UIDescription.SetDescription(item);
    }
    private void HandleShowItemActions(UIInventoryItem item)
    {

    }

    // private void HandleBeginDrag(UIInventoryItem item)
    // {
    // }

    // private void HandleEndDrag(UIInventoryItem item)
    // {
    // }

    // private void HandleSwap(UIInventoryItem item)
    // {
    // }

}
