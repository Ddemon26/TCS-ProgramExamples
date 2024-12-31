using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TCS {
    public class InventoryManager : MonoBehaviour {
        readonly List<InventoryItem> m_inventory = new();

        void Start() {
            // Add an initial item to the inventory at runtime
            AddItem("Health Potion", 3);
            AddItem("Sword", 1);

            Debug.Log("Initial Inventory:");
            DisplayInventory();
        }

        // Method to add an item or update its quantity if it already exists
        public void AddItem(string itemName, int quantity) {
            var existingItem = m_inventory.FirstOrDefault(item => item.ItemName == itemName);

            if (existingItem != null) {
                // Update quantity if item exists
                m_inventory[m_inventory.IndexOf(existingItem)] = existingItem.AddQuantity(quantity);
            }
            else {
                // Add a new item to the inventory
                m_inventory.Add(new InventoryItem { ItemName = itemName, Quantity = quantity, IsEquipped = false });
            }

            Debug.Log($"{quantity}x {itemName} added to inventory.");
        }

        // Method to equip an item
        public void EquipItem(string itemName) {
            var item = m_inventory.FirstOrDefault(i => i.ItemName == itemName);
            if (item != null && !item.IsEquipped) {
                m_inventory[m_inventory.IndexOf(item)] = item.Equip();
                Debug.Log($"{itemName} is now equipped.");
            }
            else {
                Debug.Log($"{itemName} could not be equipped.");
            }
        }

        // Method to display the inventory in the console
        void DisplayInventory() {
            foreach (var item in m_inventory) {
                Debug.Log($"Item: {item.ItemName}, Quantity: {item.Quantity}, Equipped: {item.IsEquipped}");
            }
        }

        void Update() {
            // Example runtime action: Add quantity to Health Potion by pressing 'H'
            if (Input.GetKeyDown(KeyCode.H)) {
                AddItem("Health Potion", 1);
                DisplayInventory();
            }

            // Example runtime action: Equip Sword by pressing 'E'
            if (Input.GetKeyDown(KeyCode.E)) {
                EquipItem("Sword");
                DisplayInventory();
            }
        }
    }
}