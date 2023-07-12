using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
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
            Quantity = Item == null ? 0 : quantity;
        }


        private void SetQuantity(int amount)
        {
            if (Item == null)
            {
                quantity = 0;
                return;
            }

            // Deal with negative maximum amount
            if (Item.ItemMaxAmount < 0)
            {
                quantity = 0;
                Item = null;
                return;
            }

            // Deal with overflow
            if (amount > Item.ItemMaxAmount)
            {
                quantity = item.ItemMaxAmount;
                return;
            }

            // Deal with underflow
            if (amount <= 0)
            {
                quantity = 0;
                Item = null;
                return;
            }

            quantity = amount;
        }



    }
}