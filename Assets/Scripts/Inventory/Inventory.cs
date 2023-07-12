using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Assets.Scripts.InventorySystem
{
    /// <summary>
    /// Represents an inventory system for collecting and managing items.
    /// </summary>
    [System.Serializable]
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int capacity = 0;
        [SerializeField] List<InventorySlot> slots = new List<InventorySlot>();

        public bool IsFull => CountFullSlots() == capacity;


        public int Capacity { get => capacity; set => capacity = value; }

        public Action OnInventoryChanged;
        public Action OnInventoryFull;

        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">  </param>
        /// <param name="quantity"></param>
        /// <returns> The ammount of items added </returns>
        [Button]
        public int AddItem(Item item, int quantity)
        {
            if (quantity == 0)
            {
                return 0;
            }

            if (item == null)
            {
                return 0;
            }

            if (IsFull)
            {
                OnInventoryFull?.Invoke();
                return 0;
            }

            // Add the item to a slot
            int quantityAdded = FindOrCreateSlotAndAddItem(item, quantity);

            // Check if we have leftover items
            int remainingQuantity = quantity - quantityAdded;

            // If we have no more items to add, return
            if (remainingQuantity <= 0)
            {

                return quantityAdded;
            }

            // If the inventory is still not full, recursively add items
            if (IsFull)
            {
                // Handle super overflow case where the quantity exceeds total inventory capacity
                quantityAdded += remainingQuantity;
            }
            else
            {
                quantityAdded += AddItem(item, remainingQuantity);
            }

            return quantityAdded;
        }


        int FindOrCreateSlotAndAddItem(Item item, int quantity)
        {
            InventorySlot validSlot = FindSlotFor(item, quantity);


            if (validSlot == null)
            {
                if (slots.Count < capacity && quantity > 0)
                {
                    validSlot = CreateNewSlot(item, quantity);
                    //Check for overflow
                    return (int)MathF.Min(quantity, validSlot.Item.ItemMaxAmount);
                }
                return 0;
            }

            //add the item to the slot
            validSlot.Quantity += quantity;

            if (validSlot.Item == null)
            {
                //we need to remove that slot as it is empty
                slots.Remove(validSlot);
                return (int)MathF.Min(quantity, 0);
            }

            return (int)MathF.Min(quantity, validSlot.Item.ItemMaxAmount);
        }




        [Button]
        public int Count(Item item)
        {
            if (slots.Count == 0) return 0;

            int count = 0;
            foreach (var slot in slots)
            {
                if (slot.Item != null && slot.Item.Equals(item))
                {
                    count += slot.Quantity;
                }
            }

            return count;
        }


        private InventorySlot CreateNewSlot(Item item, int quantity)
        {

            InventorySlot newSlot = new InventorySlot(item, quantity);
            slots.Add(newSlot);
            return newSlot;
        }

        public InventorySlot GetInventorySlot(int index)
        {
            if (index >= 0 && index < slots.Count)
            {
                return slots[index];
            }

            return null; // Invalid index
        }

        public void EmptyInventory()
        {
            slots.Clear();
        }


        internal bool CanAdd(Item item, int quantity)
        {
            if (IsFull)
            {
                return false;
            }

            InventorySlot validSlot = FindSlotFor(item, quantity);
            return validSlot != null;
        }






        private InventorySlot FindSlotFor(Item item, int quantity)
        {
            foreach (var slot in slots)
            {
                if (item != slot.Item) continue;

                if (slot.Quantity + quantity > slot.Item.ItemMaxAmount) continue;

                if (slot.Quantity + quantity < 0) continue;

                return slot;
            }

            return null;
        }

        private int CountFullSlots()
        {
            return (from slot in slots
                    where slot.IsFull
                    select slot).Count();
        }

    }
}