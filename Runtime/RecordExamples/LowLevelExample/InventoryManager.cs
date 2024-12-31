using System.Collections.Generic;
using UnityEngine;

namespace TCS.Tests.RecordExamples {
    public class InventoryManager : MonoBehaviour {
        readonly Dictionary<int, InventoryItem> m_items = new();

        public void AddItem(InventoryItem item) {
            if (!m_items.TryAdd(item.ID, item)) {
                UpdateItemQuantity(item.ID, item.Quantity);
            }
        }

        public void RemoveItem(int itemId, int quantity) {
            if (!m_items.TryGetValue(itemId, out var currentItem)) return;

            if (currentItem.Quantity > quantity) {
                UpdateItemQuantity(itemId, -quantity);
            }
            else {
                m_items.Remove(itemId);
            }
        }

        public InventoryItem GetItem(int itemId) {
            return m_items.GetValueOrDefault(itemId);
        }

        public void DisplayInventory() {
            foreach (var item in m_items.Values) {
                Debug.Log($"Item: {item.Name}, ID: {item.ID}, Quantity: {item.Quantity}");
            }
        }

        void UpdateItemQuantity(int itemId, int quantityChange) {
            var item = m_items[itemId];
            var updatedItem = item with { Quantity = item.Quantity + quantityChange };
            m_items[itemId] = updatedItem;
        }
    }
}