using UnityEngine;
namespace TCS {
    public class PlayerStateExample : MonoBehaviour {
        record InventoryItem(int ID, string Name, string Type);
        record PlayerState(Vector3 Position, int Health, int Score, InventoryItem[] Inventory);

        readonly PlayerState m_currentPlayerState = new
        (
            new Vector3(0, 1, 0), 100, 2500, new InventoryItem[] {
                new(1, "Sword", "Weapon"), new(2, "Potion", "Consumable"),
            }
        );

        void Start() {
            Debug.Log($"Player State: Position={m_currentPlayerState.Position}, Health={m_currentPlayerState.Health}, Score={m_currentPlayerState.Score}");
        }
    }
}