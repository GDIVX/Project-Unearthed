using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField] Item item;
        [SerializeField] int quantity;

        public Item Item { get => item; set => item = value; }
        public int Quantity { get => quantity; set => SetQuantity(value); }

        public InventorySlot(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        private void SetQuantity(int amount)
        {
            //Deal with overflow
            if (amount > item.ItemMaxAmount)
            {
                quantity = item.ItemMaxAmount;
                return;
            }

            //Deal with underflow
            if (amount < 0)
            {
                quantity = 0;
                return;
            }

            quantity = amount;
        }
    }
}