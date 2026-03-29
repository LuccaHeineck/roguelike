using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIItem : MonoBehaviour
{
    [SerializeField] private GameObject itemSelector;
    [SerializeField] private GameObject itemMarker;
    [SerializeField] private GameObject item;

    [HideInInspector] public string ID;
    [HideInInspector] public string title, description, effects;
    [HideInInspector] public Image image;
    [SerializeField] private GameObject backgroundQuantityText;
    [SerializeField] private TMP_Text quantityText;

    public event Action<UIItem>
    OnItemClicked, OnItemRightClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag, OnPointerExitItem;
    private bool empty = true;

    public void Awake()
    {
        image = item.GetComponent<Image>();

        ResetData();
        desactivateMarker();
        desactivateSelector();
        desactivateBackground();
    }

    // ======================================================

    public void SetData(InventoryStack stack)
    {
        this.gameObject.SetActive(true);
        item.gameObject.SetActive(true);
        empty = false;

        ItemData itemInv = stack.Item;

        this.ID = itemInv.ItemId;
        this.title = itemInv.ItemName;
        this.quantityText.text = stack.Quantity + "";
        this.description = itemInv.ItemDescription;
        this.effects = itemInv.ItemEffectText;
        this.image.sprite = itemInv.ItemSprite;
    }

    public void ResetData()
    {
        item.SetActive(false);
        empty = true;
    }

    public void ConvertItemIntoUIItem(InventoryStack stack)
    {
        ItemData item = stack.Item;

        this.ID = item.ItemId;
        this.title = item.ItemName;
        this.quantityText.text = stack.Quantity.ToString();
        this.description = item.ItemDescription;
        this.effects = item.ItemEffectText;
        this.image.sprite = item.ItemSprite;
    }

    public void UpdateData(InventoryStack stack)
    {
        this.quantityText.text = stack.Quantity.ToString();
        if (this.quantityText.text != "1")
            activateBackground();
    }

    // ======================================================

    public void selectItemFromInventory() => this.activateSelector();

    public void activateSelector() => this.itemSelector.gameObject.SetActive(true);
    public void desactivateSelector() => this.itemSelector.gameObject.SetActive(false);

    public void activateMarker() => this.itemMarker.gameObject.SetActive(true);
    public void desactivateMarker() => this.itemMarker.gameObject.SetActive(false);

    private void activateBackground() => this.backgroundQuantityText.SetActive(true);
    private void desactivateBackground() => this.backgroundQuantityText.SetActive(false);


    // ======================================================

    public void OnPointerEnter()
    {
        if (empty || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            return;
        activateMarker();
    }

    public void OnPointerExit()
    {
        if (empty || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            return;
        desactivateMarker();
        OnPointerExitItem?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;

        if (this.empty) return;

        if (pointerData.button == PointerEventData.InputButton.Right)
            OnItemRightClicked?.Invoke(this);
        else
            OnItemClicked?.Invoke(this);

    }

    // public void OnBeginDrag()
    // {
    //     if (empty)
    //         return;
    //     OnItemBeginDrag?.Invoke(this);
    // }

    // public void OnEndDrag()
    // {
    //     OnItemEndDrag?.Invoke(this);
    // }

    // public void OnDroppedOn()
    // {
    //     OnItemDroppedOn?.Invoke(this);
    // }
}
