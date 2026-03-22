using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private GameObject itemSelector;
    [SerializeField] private Sprite spriteItemSelector;
    [SerializeField] private Sprite spriteItemSelectorClick;
    [SerializeField] private TMP_Text itemQuantity;
    [SerializeField] private GameObject item;
    private Image itemImage;
    [HideInDebugUI] public string title, description, effects;

    public event Action<InventoryItem>
    OnItemClicked, OnItemRightClicked, OnItemDroppedOn, OnItemBeginDrag, OnItemEndDrag;

    private bool empty = true;

    public void Awake()
    {
        itemImage = item.GetComponent<Image>();
        itemSelector.GetComponent<Image>().sprite = spriteItemSelector;

        ResetData();
        desactivateSelector();
    }

    public void ResetData()
    {
        item.SetActive(false);
        empty = true;
    }

    public void SetData(Sprite itemSprite, int quantity)
    {
        item.SetActive(true);
        itemImage.sprite = itemSprite;
        itemQuantity.text = quantity + "";
        empty = false;
    }

    public void selectItemFromInventory()
    {
        itemSelector.GetComponent<Image>().sprite = spriteItemSelectorClick;
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

    public void OnBeginDrag()
    {
        if (empty)
            return;
        itemSelector.GetComponent<Image>().sprite = spriteItemSelectorClick;
        activateSelector();
        OnItemBeginDrag?.Invoke(this);
    }

    public void OnEndDrag()
    {
        itemSelector.GetComponent<Image>().sprite = spriteItemSelector;
        desactivateSelector();
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDroppedOn()
    {
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        if (pointerData.button == PointerEventData.InputButton.Right)
            OnItemRightClicked?.Invoke(this);
        else
            OnItemClicked?.Invoke(this);
    }
}
