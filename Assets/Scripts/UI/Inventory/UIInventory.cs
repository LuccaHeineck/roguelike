using System.Collections.Generic;
using UnityEngine;


public class UIInventory : MonoBehaviour
{
    [SerializeField] private UIItem UIItemPrefab;
    [SerializeField] private RectTransform UIInventoryGrid;
    public UIDescription UIDescription;
    public Sprite defaultSlotSprite;

    private UIInventoryControl UIInvControl;
    private List<UIItem> ListUIItems = new List<UIItem>();

    int lastItemSelected = 0;
    int UIInventorySize = 0;

    public void SetInventoryControl(UIInventoryControl UIInvControl) => this.UIInvControl = UIInvControl;

    void Awake()
    {
        HideInventory();
        UIDescription.SetUIInventory(this);
    }

    //==============================================================================

    public void InitializeUIInventory(int maxSlots)
    {
        destroyChildren(UIInventoryGrid);
        foreach (var item in ListUIItems) Destroy(item.gameObject);
        ListUIItems.Clear();

        for (int i = 0; i < maxSlots; i++)
        {
            UIItem uiItem = Instantiate(UIItemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(UIInventoryGrid);

            if (i < UIInventorySize)
                uiItem.ConvertItemIntoUIItem(UIInvControl.pInventory.Stacks[i]);

            ListUIItems.Add(uiItem);

            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemRightClicked += HandleShowItemActions;
            // uiItem.OnItemBeginDrag += HandleBeginDrag;
            // uiItem.OnItemEndDrag += HandleEndDrag;
            // uiItem.OnItemDroppedOn += HandleSwap;
        }

        HideInventory();
    }

    private void destroyChildren(Transform parent)
    {
        foreach (Transform child in parent)
            GameObject.Destroy(child.gameObject);

    }

    public void UpdateUIInventory()
    {
        int inventorySIze = UIInvControl.pInventory.Stacks.Count;

        if (inventorySIze > UIInventorySize)
        {
            ListUIItems[inventorySIze - 1].SetData(UIInvControl.pInventory.Stacks[inventorySIze - 1]);
            UIInventorySize++;
            return;
        }

        int itemIndex = 0;

        while (itemIndex < inventorySIze)
        {
            ListUIItems[itemIndex].UpdateData(UIInvControl.pInventory.Stacks[itemIndex]);
            itemIndex++;
        }
    }

    //==============================================================================

    public bool IsVisible() => gameObject.activeSelf;

    public void ShowInventory()
    {
        UIDescription.ResetDescription();
        gameObject.SetActive(true);
    }

    public void HideInventory() => gameObject.SetActive(false);

    //==============================================================================

    private void HandleItemSelection(UIItem item)
    {
        if (item == null) return;

        ListUIItems[lastItemSelected].desactivateSelector();
        lastItemSelected = item.transform.GetSiblingIndex();
        item.activateSelector();

        UIDescription.SetDescription(item);
    }

    private void HandleShowItemActions(UIItem item)
    {

    }

    // private void HandleBeginDrag(UIItem item)
    // {
    // }

    // private void HandleEndDrag(UIItem item)
    // {
    // }

    // private void HandleSwap(UIItem item)
    // {
    // }

}
