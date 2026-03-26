using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField] private GameObject itemSelector;
    [SerializeField] private GameObject itemMarker;
    [SerializeField] private GameObject item;

    [HideInInspector] public string title, description, effects;
    [HideInInspector] public Image image;
    public TMP_Text quantityText;

    public event Action<UIInventoryItem>
    OnItemClicked, OnItemRightClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    public void Awake()
    {
        image = item.GetComponent<Image>();

        ResetData();
        desactivateMarker();
        desactivateSelector();
    }

    // ======================================================

    public void ConvertItemIntoUIItem(InventoryStack stack)
    {
        ItemData item = stack.Item;

        this.title = item.ItemName;
        this.quantityText.text = stack.Quantity.ToString();
        this.description = item.ItemDescription;
        this.effects = item.ItemEffectText;
        this.image.sprite = item.ItemSprite;
    }

    // ======================================================

    public void ResetData()
    {
        item.SetActive(false);
        empty = true;
    }

    public void SetData(Sprite itemSprite, int quantity)
    {
        item.SetActive(true);
        image.sprite = itemSprite;
        quantityText.text = quantity + "";
        empty = false;
    }

    // ======================================================

    public void selectItemFromInventory()
    {
        activateSelector();
    }

    public void activateSelector()
    {
        itemSelector.gameObject.SetActive(true);
    }

    public void desactivateSelector()
    {
        itemSelector.gameObject.SetActive(false);
    }

    public void activateMarker()
    {
        itemSelector.gameObject.SetActive(true);
    }

    public void desactivateMarker()
    {
        itemSelector.gameObject.SetActive(false);
    }

    // ======================================================

    public void OnPointerEnter()
    {
        if (empty || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            return;
        activateSelector();
    }

    public void OnPointerExit()
    {
        if (empty || Mouse.current.leftButton.isPressed || Mouse.current.rightButton.isPressed)
            return;
        desactivateSelector();
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
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
