using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{

    /// <summary>
    /// This class is used to pick up items in the world.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ItemPickup : MonoBehaviour, IPickup
    {
        [SerializeField] InventorySlot content;

        public InventorySlot Content { get => content; set => content = value; }



        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Inventory inventory = other.GetComponentInChildren<Inventory>();

                int remainingQuantity = Content.Quantity;
                int addedQuantity = 0;

                // Try to add as much as possible from the pickup to the inventory
                while (remainingQuantity > 0)
                {
                    int quantityToAdd = Mathf.Min(remainingQuantity, Content.Item.ItemMaxAmount - addedQuantity);

                    int quantityAdded = inventory.AddItem(Content.Item, quantityToAdd);
                    addedQuantity += quantityAdded;
                    remainingQuantity -= quantityAdded;

                    if (quantityAdded <= 0)
                    {
                        // Inventory is full, break out of the loop
                        break;
                    }
                }

                // Update the pickup's content with the remaining quantity
                Content.Quantity = remainingQuantity;

                if (Content.Quantity <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }



        /// <summary>
        /// Splits the item pickup into two item pickups
        /// </summary>
        /// <param name="amountToTransfer"> How much of the item stack to move from the original copy to the new copy </param>
        /// <returns>A tuple with two item pickups</returns>
        public Tuple<ItemPickup, ItemPickup> Split(int amountToTransfer)
        {
            if (amountToTransfer <= 0)
            {
                // Return only the original pickup unmodified
                return Tuple.Create(this, (ItemPickup)null);
            }

            // Ensure valid splitting conditions
            amountToTransfer = Mathf.Clamp(amountToTransfer, 1, Content.Quantity);

            // Adjust the quantity of the original item pickup
            Content.Quantity -= amountToTransfer;

            // Create the new item pickup using the factory method
            ItemPickup newPickup = CreateItemPickup(Content.Item, transform.position + new Vector3(0.5f, 0f, 0f), amountToTransfer);

            // Return a tuple with the original pickup and the new pickup
            return Tuple.Create(this, newPickup);
        }


        //Factory method for creating a pickup from an item
        [Button]
        public static ItemPickup CreateItemPickup(Item item, Vector3 position, int stackSize = 1)
        {
            GameObject itemPickupGameObject = new GameObject(item.ItemName + " Pickup");
            itemPickupGameObject.tag = "Pickup";

            // setup the sprite renderer
            SpriteRenderer spriteRenderer = itemPickupGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.ItemSprite;

            // setup the collider
            BoxCollider triggerCollider = itemPickupGameObject.AddComponent<BoxCollider>();
            triggerCollider.isTrigger = true;
            triggerCollider.size = new(0.5f, 0.5f, 0.5f);


            ItemPickup itemPickup = itemPickupGameObject.AddComponent<ItemPickup>();
            itemPickup.Content = new(item, stackSize);

            itemPickupGameObject.transform.position = position;

            return itemPickup;
        }

        public bool CanPickup(Inventory inventory)
        {
            return inventory.CanAdd(content.Item, content.Quantity);
        }
    }
}