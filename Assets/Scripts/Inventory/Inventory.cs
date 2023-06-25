using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    /// Represents an inventory system for collecting and managing items.
    /// </summary>
    [System.Serializable]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<InventorySlot> inventorySlots;

        public UnityEvent<Inventory, Item, int> OnInventoryChanged;
        public UnityEvent<Inventory> OnInventoryFull;

        /// <summary>
        /// Gets the list of inventory slots.
        /// </summary>
        public List<InventorySlot> InventorySlots { get => inventorySlots; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inventory"/> class.
        /// </summary>
        public Inventory()
        {
            inventorySlots = new List<InventorySlot>();
        }

        /// <summary>
        /// Adds an item and its quantity to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>The amount that was successfully added.</returns>
        [Button]
        public int AddItem(Item item, int quantity)
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

        /// <summary>
        /// Adds an item from an inventory slot to the inventory.
        /// </summary>
        /// <param name="content">The inventory slot to add.</param>
        /// <returns>The amount that was successfully added.</returns>
        internal int AddItem(InventorySlot content)
        {
            return AddItem(content.Item, content.Quantity);
        }

        /// <summary>
        /// Determines if an item can be added to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="quantity">The quantity to add.</param>
        /// <returns>True if the item can be added, false otherwise.</returns>
        public bool CanAdd(Item item, int quantity)
        {
            if (item.ItemMaxAmount <= 1)
            {
                foreach (InventorySlot slot in inventorySlots)
                {
                    if (slot.Item == null)
                    {
                        return true;
                    }
                }
            }
            else
            {
                foreach (InventorySlot slot in inventorySlots)
                {
                    if (slot.Item == item && slot.Quantity + quantity <= item.ItemMaxAmount)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Removes an item and its quantity from the inventory.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <param name="quantity">The quantity to remove.</param>
        /// <returns>True if the item was successfully removed, false otherwise.</returns>
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
                        OnInventoryChanged?.Invoke(this, item, -quantity);
                        return true;
                    }
                }
            }

            return false; // Item not found in the inventory
        }

        /// <summary>
        /// Gets the inventory slot at the specified index.
        /// </summary>
        /// <param name="index">The index of the inventory slot.</param>
        /// <returns>The inventory slot at the specified index.</returns>
        [Button]
        public InventorySlot GetInventorySlot(int index)
        {
            if (index >= 0 && index < inventorySlots.Count)
            {
                return inventorySlots[index];
            }

            return null; // Invalid index
        }

        private int AddItemToEmptySlot(Item item, int quantity)
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
                return 0;
            }

            //We added all of the items
            OnInventoryChanged?.Invoke(this, item, quantity);
            return quantity;
        }

        private int StackItemInExistingSlots(Item item, int quantity)
        {
            int remainingQuantity = quantity;
            foreach (InventorySlot slot in inventorySlots)
            {
                if (slot.Item == item)
                {
                    int remainingSpace = item.ItemMaxAmount - slot.Quantity;

                    if (remainingSpace > 0)
                    {
                        int quantityToAdd = Mathf.Min(quantity, remainingSpace);
                        slot.Quantity += quantityToAdd;
                        remainingQuantity -= quantityToAdd;

                        if (quantity == 0)
                        {
                            OnInventoryChanged?.Invoke(this, item, quantityToAdd);
                            return quantity; // Added all items to existing slots
                        }
                    }
                }
            }
            // Add remaining items to empty slots
            //Return the remaining items we can add and those we already did
            return AddItemToEmptySlot(item, remainingQuantity) + (quantity - remainingQuantity);
        }

    }
}