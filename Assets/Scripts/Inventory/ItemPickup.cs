using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Inventory
{

    /// <summary>
    /// This class is used to pick up items in the world.
    /// </summary>
    [RequireComponent(typeof(Collider))]
    public class ItemPickup : MonoBehaviour
    {
        [SerializeField] Item item;
        [SerializeField] int stackSize = 1;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Get the inventory component 

                Inventory inventory = other.GetComponentInChildren<Inventory>();

                inventory.AddItem(item, stackSize);

                Destroy(gameObject);
            }
        }

        //Factory method for creating a pickup from an item
        [Button]
        public static ItemPickup CreateItemPickup(Item item, Vector3 position, int stackSize = 1)
        {
            GameObject itemPickupGameObject = new GameObject(item.ItemName + " Pickup");

            // setup the sprite renderer
            SpriteRenderer spriteRenderer = itemPickupGameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.ItemSprite;

            // setup the collider
            BoxCollider triggerCollider = itemPickupGameObject.AddComponent<BoxCollider>();
            triggerCollider.isTrigger = true;
            triggerCollider.size = new(0.5f, 0.5f, 0.5f);

            BoxCollider Collider = itemPickupGameObject.AddComponent<BoxCollider>();
            Collider.size = new(0.5f, 0.5f, 0.5f);

            //add rigidbody
            itemPickupGameObject.AddComponent<Rigidbody>();

            ItemPickup itemPickup = itemPickupGameObject.AddComponent<ItemPickup>();
            itemPickup.item = item;
            itemPickup.stackSize = stackSize;

            itemPickupGameObject.transform.position = position;

            return itemPickup;
        }
    }
}