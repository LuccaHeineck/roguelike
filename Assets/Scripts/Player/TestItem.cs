// ItemTester.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class ItemTester : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int addAmountPerPress = 1;
    [SerializeField] private int removeAmountPerPress = 1;

    private PlayerStats stats;
    private PlayerInventory inventory;
    private ItemEffectController controller;

    private void Awake()
    {
        stats = GetComponent<PlayerStats>();
        inventory = GetComponent<PlayerInventory>();
        controller = GetComponent<ItemEffectController>();
    }

    private void Update()
    {
        if (itemData == null || inventory == null || stats == null)
        {
            return;
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            int added = inventory.TryAddItem(itemData, addAmountPerPress);
            int total = inventory.GetTotalQuantity(itemData);
            int damageStat = stats.CurrentDamage;
            Debug.Log($"[ADICIONADO] +{added} {itemData.ItemName} | Total: {total} | DamageStat: {damageStat}");
        }

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            int removed = inventory.TryRemoveItem(itemData, removeAmountPerPress);
            int total = inventory.GetTotalQuantity(itemData);
            int damageStat = stats.CurrentDamage;
            Debug.Log($"[REMOVIDO] -{removed} {itemData.ItemName} | Total: {total} | DamageStat: {damageStat}");
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            int stacks = controller.GetRuntimeStackCount(itemData);
            int total = inventory.GetTotalQuantity(itemData);
            int damageStat = stats.CurrentDamage;
            Debug.Log($"[INFO] RuntimeStacks: {stacks} | ItemTotal: {total} | Slots: {inventory.OccupiedSlots}/{inventory.MaxSlots} | DamageStat: {damageStat}");
        }
    }
}