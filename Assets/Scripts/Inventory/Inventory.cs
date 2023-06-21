using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    [SerializeField, HideInInspector]
    private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots { get => inventorySlots; }

    public Inventory()
    {
        inventorySlots = new List<InventorySlot>();
    }

    [Button]
    public bool AddItem(Item item, int quantity)
    {
        if (item.ItemMaxAmount <= 1)
        {
            return AddItemToEmptySlot(item, quantity);
        }
        else
        {
            return StackItemInExistingSlots(item, quantity);
        }
    }

    private bool AddItemToEmptySlot(Item item, int quantity)
    {
        inventorySlots.Add(new InventorySlot(item, quantity));
        return true;
    }

    private bool StackItemInExistingSlots(Item item, int quantity)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.Item == item)
            {
                int remainingSpace = item.ItemMaxAmount - slot.Quantity;

                if (remainingSpace > 0)
                {
                    int quantityToAdd = Mathf.Min(quantity, remainingSpace);
                    slot.Quantity += quantityToAdd;
                    quantity -= quantityToAdd;

                    if (quantity == 0)
                    {
                        return true; // Added all items to existing slots
                    }
                }
            }
        }

        return AddItemToEmptySlot(item, quantity); // Add remaining items to empty slots
    }

    [Button]
    public bool RemoveItem(Item item, int quantity)
    {
        for (int i = inventorySlots.Count - 1; i >= 0; i--)
        {
            InventorySlot slot = inventorySlots[i];

            if (slot.Item == item)
            {
                if (slot.Quantity > quantity)
                {
                    slot.Quantity -= quantity;
                    return true;
                }
                else
                {
                    inventorySlots.RemoveAt(i);
                    return true;
                }
            }
        }

        return false; // Item not found in the inventory
    }

    [Button]
    public InventorySlot GetInventorySlot(int index)
    {
        if (index >= 0 && index < inventorySlots.Count)
        {
            return inventorySlots[index];
        }

        return null; // Invalid index
    }
}
