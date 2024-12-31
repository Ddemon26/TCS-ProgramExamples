using UnityEngine;
namespace TCS {
    public class InventoryItemExample : MonoBehaviour {
        public enum ItemType { Weapon, Consumable, CraftingMaterial }
        record InventoryItem(int ID, string Name, ItemType Type);

        readonly InventoryItem m_sword = new(1, "Sword", ItemType.Weapon);
        InventoryItem m_potion = new(2, "Health Potion", ItemType.Consumable);

        void Start() {
            Debug.Log($"Inventory Item: {m_sword.Name}, Type={m_sword.Type}");
        }
    }
}