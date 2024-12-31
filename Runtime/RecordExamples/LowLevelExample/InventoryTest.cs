using UnityEngine;
namespace TCS.Tests.RecordExamples {
    public class InventoryTest : MonoBehaviour {
        InventoryManager m_inventoryManager;

        void Start() {
            m_inventoryManager = FindFirstObjectByType<InventoryManager>(FindObjectsInactive.Include);

            var item1 = new InventoryItem("Health Potion", 1, 5);
            var item2 = new InventoryItem("Mana Potion", 2, 3);

            m_inventoryManager.AddItem(item1);
            m_inventoryManager.AddItem(item2);

            m_inventoryManager.DisplayInventory();

            m_inventoryManager.AddItem(new InventoryItem("Health Potion", 1, 2));
            m_inventoryManager.RemoveItem(2, 1);

            m_inventoryManager.DisplayInventory();
        }
    }
}