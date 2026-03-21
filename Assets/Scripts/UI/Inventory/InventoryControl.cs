using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryControl : MonoBehaviour
{
    [SerializeField] private InventoryPage invetoryUI;
    public int inventorySize = 0;

    public void Start() => invetoryUI.initializeInventoryUI(inventorySize);

    public void Update()
    {
        if (Keyboard.current.iKey.isPressed || Keyboard.current.tabKey.isPressed)
        {
            if (!invetoryUI.IsVisible())
                invetoryUI.ShowInventory();
            return;
        }
        if (invetoryUI.IsVisible())
            invetoryUI.HideInventory();
    }
}
