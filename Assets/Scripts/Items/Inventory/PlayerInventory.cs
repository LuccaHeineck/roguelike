using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * PlayerInventory e a fonte da verdade do inventario no runtime.
 * Ele controla stacks, quantidade total, limite de slots e aplica/remove efeitos automaticamente.
 */

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private int maxSlots = 24;
    [SerializeField] private int defaultMaxStackPerItem = 99;
    [SerializeField] private PlayerStats playerStats;

    private readonly List<InventoryStack> stacks = new List<InventoryStack>();

    public event Action OnInventoryChanged;

    public IReadOnlyList<InventoryStack> Stacks => stacks;
    public int MaxSlots => maxSlots;
    public int OccupiedSlots => stacks.Count;

    private void Awake()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }
    }

    public int GetTotalQuantity(ItemData itemData)
    {
        if (itemData == null)
        {
            return 0;
        }

        int total = 0;
        for (int i = 0; i < stacks.Count; i++)
        {
            if (stacks[i].Item == itemData)
            {
                total += stacks[i].Quantity;
            }
        }

        return total;
    }

    public bool Contains(ItemData itemData)
    {
        return GetTotalQuantity(itemData) > 0;
    }

    public int GetUniqueItemCount()
    {
        HashSet<ItemData> uniqueItems = new HashSet<ItemData>();
        for (int i = 0; i < stacks.Count; i++)
        {
            if (stacks[i].Item != null)
            {
                uniqueItems.Add(stacks[i].Item);
            }
        }

        return uniqueItems.Count;
    }

    public int TryAddItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return 0;
        }

        int stackLimit = Mathf.Max(1, defaultMaxStackPerItem);
        int remaining = amount;

        for (int i = 0; i < stacks.Count; i++)
        {
            InventoryStack stack = stacks[i];
            if (stack.Item != itemData || stack.Quantity >= stackLimit)
            {
                continue;
            }

            int freeSpace = stackLimit - stack.Quantity;
            int toAdd = Mathf.Min(freeSpace, remaining);

            stack.Quantity += toAdd;
            remaining -= toAdd;

            ApplyEffects(itemData, toAdd);

            if (remaining <= 0)
            {
                RaiseChanged();
                return amount;
            }
        }

        while (remaining > 0 && stacks.Count < maxSlots)
        {
            int toAdd = Mathf.Min(stackLimit, remaining);
            stacks.Add(new InventoryStack(itemData, toAdd));
            remaining -= toAdd;

            ApplyEffects(itemData, toAdd);
        }

        int added = amount - remaining;
        if (added > 0)
        {
            RaiseChanged();
        }

        return added;
    }

    public int TryRemoveItem(ItemData itemData, int amount)
    {
        if (itemData == null || amount <= 0)
        {
            return 0;
        }

        int remaining = amount;

        for (int i = stacks.Count - 1; i >= 0 && remaining > 0; i--)
        {
            InventoryStack stack = stacks[i];
            if (stack.Item != itemData)
            {
                continue;
            }

            int toRemove = Mathf.Min(stack.Quantity, remaining);
            stack.Quantity -= toRemove;
            remaining -= toRemove;

            RemoveEffects(itemData, toRemove);

            if (stack.Quantity <= 0)
            {
                stacks.RemoveAt(i);
            }
        }

        int removed = amount - remaining;
        if (removed > 0)
        {
            RaiseChanged();
        }

        return removed;
    }

    public void ClearInventory()
    {
        if (stacks.Count == 0)
        {
            return;
        }

        for (int i = 0; i < stacks.Count; i++)
        {
            InventoryStack stack = stacks[i];
            if (stack.Item == null || stack.Quantity <= 0)
            {
                continue;
            }

            RemoveEffects(stack.Item, stack.Quantity);
        }

        stacks.Clear();
        RaiseChanged();
    }

    private void ApplyEffects(ItemData itemData, int stackCount)
    {
        if (playerStats == null || stackCount <= 0)
        {
            return;
        }

        ItemEffectApplier.ApplyItemStack(itemData, playerStats, stackCount);
    }

    private void RemoveEffects(ItemData itemData, int stackCount)
    {
        if (playerStats == null || stackCount <= 0)
        {
            return;
        }

        ItemEffectApplier.RemoveItemStack(itemData, playerStats, stackCount);
    }

    private void RaiseChanged()
    {
        OnInventoryChanged?.Invoke();
    }
}
