using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventorySystem
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemData")]
    public class Item : ScriptableObject
    {
        [SerializeField] private string itemName;
        [SerializeField] private string itemDescription;
        [SerializeField] private Sprite itemSprite;
        [SerializeField] private int itemMaxAmount;

        public string ItemName { get => itemName; }
        public string ItemDescription { get => itemDescription; }
        public Sprite ItemSprite { get => itemSprite; }
        public int ItemMaxAmount { get => itemMaxAmount; }

        /// <summary>
        /// Creates a new Item with the given parameters.
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemDescription"></param>
        /// <param name="itemSprite"></param>
        /// <param name="itemMaxAmount"></param>
        /// <returns></returns>
        [Button]
        public static Item CreateItem(string itemName, string itemDescription, Sprite itemSprite, int itemMaxAmount)
        {
            Item newItem = CreateInstance<Item>();
            newItem.itemName = itemName;
            newItem.itemDescription = itemDescription;
            newItem.itemSprite = itemSprite;
            newItem.itemMaxAmount = itemMaxAmount;
            return newItem;
        }
    }
}