using System;

[Serializable]
public class InventoryStack
{
    public ItemData Item;
    public int Quantity;

    public InventoryStack(ItemData item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }
}
