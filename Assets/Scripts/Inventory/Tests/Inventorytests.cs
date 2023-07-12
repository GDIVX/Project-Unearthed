using Assets.Scripts.InventorySystem;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class InventoryTests
    {
        private GameObject gameObject;
        private Inventory inventory;

        [SetUp]
        public void Setup()
        {
            // Create a new GameObject and attach the Inventory component
            gameObject = new GameObject();
            inventory = gameObject.AddComponent<Inventory>();
        }

        [TearDown]
        public void TearDown()
        {
            // Destroy the GameObject after each test
            Object.Destroy(gameObject);
        }

        [Test]
        public void AddItem_InsufficientCapacity_ReturnsZero()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 1;
            inventory.AddItem(item, 10); // Fill up the inventory slot

            // Act
            int result = inventory.AddItem(item, 5); // Attempt to add more items than capacity

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AddItem_CanAddItem_ReturnsQuantityAdded()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 2;

            // Act
            int result = inventory.AddItem(item, 5);

            // Assert
            Assert.AreEqual(5, result);
        }

        [Test]
        public void AddItem_FullInventory_InvokesOnInventoryFull()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 1;
            bool eventInvoked = false;
            inventory.OnInventoryFull += () => eventInvoked = true;

            // Act
            inventory.AddItem(item, 10);
            inventory.AddItem(item, 5);

            // Assert
            Assert.IsTrue(eventInvoked);
        }

        [Test]
        public void AddItem_LimitedCapacity_ReturnsQuantityAdded()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 1;

            // Act
            int quantityAdded = inventory.AddItem(item, 5);

            // Assert
            Assert.AreEqual(5, quantityAdded);
        }

        [Test]
        public void AddItem_AddsCorrectQuantityToSlot()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 1;
            int quantityToAdd = 3;

            // Act
            inventory.AddItem(item, quantityToAdd);

            // Assert
            InventorySlot slot = inventory.GetInventorySlot(0);
            Assert.AreEqual(quantityToAdd, slot.Quantity);
        }


        [Test]
        public void Count_ReturnsCorrectItemCount()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 2;

            // Act
            inventory.AddItem(item, 5);
            int itemCount = inventory.Count(item);

            // Assert
            Assert.AreEqual(5, itemCount);
        }

        [Test]
        public void AddItem_RegularOverflow_AddsItemsToAvailableSlots()
        {
            // Arrange
            Item item = Item.Mock(10); // Item with max amount of 10
            int quantityToAdd = 25; // Quantity that requires 3 slots

            inventory.Capacity = 5; // Set inventory capacity to 5

            // Act
            int addedQuantity = inventory.AddItem(item, quantityToAdd);

            // Assert
            Assert.AreEqual(25, addedQuantity); // Total of 25 items were added
            Assert.AreEqual(10, inventory.GetInventorySlot(0).Quantity); // Slot 0 has 10 items
            Assert.AreEqual(10, inventory.GetInventorySlot(1).Quantity); // Slot 1 has 10 items
            Assert.AreEqual(5, inventory.GetInventorySlot(2).Quantity); // Slot 2 has 5 items
        }


        [Test]
        public void AddItem_SuperOverflow_AddsItemsToAvailableSlotsAndReturnsTotalAddedQuantity()
        {
            // Arrange
            Item item = Item.Mock(10); // Item with max amount of 10
            int quantityToAdd = 55; // Quantity that requires 6 slots (5 filled slots + 1 additional)

            inventory.Capacity = 5; // Set inventory capacity to 5

            // Act
            int addedQuantity = inventory.AddItem(item, quantityToAdd);

            // Assert
            Assert.AreEqual(55, addedQuantity); // Total of 55 items were added
            Assert.AreEqual(10, inventory.GetInventorySlot(0).Quantity); // Slot 0 has 10 items
            Assert.AreEqual(10, inventory.GetInventorySlot(1).Quantity); // Slot 1 has 10 items
            Assert.AreEqual(10, inventory.GetInventorySlot(2).Quantity); // Slot 2 has 10 items
            Assert.AreEqual(10, inventory.GetInventorySlot(3).Quantity); // Slot 3 has 10 items
            Assert.AreEqual(10, inventory.GetInventorySlot(4).Quantity); // Slot 4 has 10 items
        }

        [Test]
        public void RemoveItem_ReduceQuantity_RemovesCorrectQuantity()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 5;
            inventory.AddItem(item, 10);

            // Act
            int removedQuantity = inventory.AddItem(item, -5);

            // Assert
            Assert.AreEqual(-5, removedQuantity);
            Assert.AreEqual(5, inventory.GetInventorySlot(0).Quantity); // Slot 0 has 5 items remaining
        }

        [Test]
        public void RemoveItem_CompletelyRemoveItem_RemovesItemFromInventory()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 5;
            inventory.AddItem(item, 10);

            // Act
            int removedQuantity = inventory.AddItem(item, -10);

            // Assert
            Assert.AreEqual(-10, removedQuantity);
            Assert.IsNull(inventory.GetInventorySlot(0)); // Slot 0 is now empty
        }

        [Test]
        public void EmptyInventory_CountReturnsZero()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 5;
            inventory.AddItem(item, 10);

            // Act
            inventory.EmptyInventory();

            // Assert
            Assert.AreEqual(0, inventory.Count(item));
        }

        [Test]
        public void AddItem_NegativeQuantity_ReturnsZero()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 5;

            // Act
            int result = inventory.AddItem(item, -5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AddItem_NullItem_ReturnsZero()
        {
            // Arrange
            inventory.Capacity = 5;

            // Act
            int result = inventory.AddItem(null, 5);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void AddItem_ExceedCapacity_InvokesOnInventoryFull()
        {
            // Arrange
            Item item = Item.Mock(10);
            inventory.Capacity = 1;
            bool eventInvoked = false;
            inventory.OnInventoryFull += () => eventInvoked = true;

            // Act
            inventory.AddItem(item, 10);
            inventory.AddItem(item, 5); // Attempt to add more items than capacity

            // Assert
            Assert.IsTrue(eventInvoked);
        }

        [Test]
        public void RemoveItem_Underflow_ReturnsZero()
        {
            // Arrange
            Item item = Item.Mock(10); // Item with max amount of 10
            inventory.Capacity = 1;
            inventory.AddItem(item, 5); // Add 5 items to the inventory

            // Act
            int removedQuantity = inventory.AddItem(item, -10); // Attempt to remove more items than available

            // Assert
            Assert.AreEqual(0, removedQuantity); // No items should be removed
            Assert.AreEqual(5, inventory.GetInventorySlot(0).Quantity); // The quantity in the slot should remain unchanged
        }


    }
}
