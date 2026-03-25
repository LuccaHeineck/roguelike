using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryControl : MonoBehaviour
{
    [SerializeField] private InventoryPage inventoryUI;
    public int inventoryMaxSize = 0;

    public void Start() => inventoryUI.initializeInventoryUI(inventoryMaxSize);

    public void Update()
    {
        if (Keyboard.current.iKey.isPressed || Keyboard.current.tabKey.isPressed)
        {
            if (!inventoryUI.IsVisible())
                inventoryUI.ShowInventory();
            return;
        }

        if (inventoryUI.IsVisible())
        {
            inventoryUI.HideInventory();
            inventoryUI.itemDescription.ResetDescription();
        }
    }
}
