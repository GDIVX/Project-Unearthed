using NUnit.Framework;
using UnityEngine;
using Assets.Scripts.InventorySystem;

namespace Tests
{
    public class InventorySlotTests
    {
        [Test]
        public void SetQuantity_Overflow_CapsQuantity()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = 15;

            // Assert
            Assert.AreEqual(item.ItemMaxAmount, inventorySlot.Quantity);
        }

        [Test]
        public void SetQuantity_Underflow_ClearsItem()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = -5;

            // Assert
            Assert.AreEqual(0, inventorySlot.Quantity);
            Assert.IsNull(inventorySlot.Item);
        }

        [Test]
        public void SetQuantity_ValidAmount_SetsQuantity()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = 8;

            // Assert
            Assert.AreEqual(8, inventorySlot.Quantity);
        }

        [Test]
        public void SetQuantity_ValidAmount_ItemNotNull()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = 3;

            // Assert
            Assert.IsNotNull(inventorySlot.Item);
        }


        [Test]
        public void SetQuantity_MaximumQuantity_CapsToMaxQuantity()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = item.ItemMaxAmount + 5;

            // Assert
            Assert.AreEqual(item.ItemMaxAmount, inventorySlot.Quantity);
        }

        [Test]
        public void SetQuantity_MinimumQuantity_CapsToZero()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = -10;

            // Assert
            Assert.AreEqual(0, inventorySlot.Quantity);
        }

        [Test]
        public void SetQuantity_NegativeMaxAmount_ClearsSlot()
        {
            // Arrange
            Item item = Item.Mock(-10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = 10;

            // Assert
            Assert.AreEqual(0, inventorySlot.Quantity);
            Assert.IsNull(inventorySlot.Item);
        }







        [Test]
        public void SetQuantity_DecrementQuantity_RemovesItems()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity -= 3;

            // Assert
            Assert.AreEqual(2, inventorySlot.Quantity);
        }

        [Test]
        public void SetQuantity_DecrementToZero_ClearsItem()
        {
            // Arrange
            Item item = Item.Mock(10);
            InventorySlot inventorySlot = new InventorySlot(item, 5);

            // Act
            inventorySlot.Quantity = inventorySlot.Quantity - inventorySlot.Quantity;

            // Assert
            Assert.AreEqual(0, inventorySlot.Quantity);
            Assert.IsNull(inventorySlot.Item);
        }

    }
}
