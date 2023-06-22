using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Inventory
{
    [System.Serializable]
    public class Inventory : MonoBehaviour
    {

        [SerializeField] private List<InventorySlot> inventorySlots;

        public UnityEvent<Inventory> OnInventoryChanged;
        public UnityEvent<Inventory> OnInventoryFull;

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
            bool slotFound = false;

            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.Item == null)
                {
                    slot.Item = item;
                    slot.Quantity = quantity;
                    slotFound = true;
                    break;
                }
            }

            if (!slotFound)
            {
                // Invoke an event for no empty slot found
                OnInventoryFull?.Invoke(this);
                return false;
            }

            OnInventoryChanged?.Invoke(this);
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
                            OnInventoryChanged?.Invoke(this);
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
                        OnInventoryChanged?.Invoke(this);
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
}