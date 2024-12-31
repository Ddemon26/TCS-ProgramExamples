using UnityEngine;

namespace TCS {
    public class GameManager : MonoBehaviour {
        void OnEnable() {
            EventBus.Instance.Subscribe<PlayerScoredEvent>(OnPlayerScored);
            EventBus.Instance.Subscribe<ItemCollectedEvent>(OnItemCollected);
        }

        void OnDisable() {
            EventBus.Instance.Unsubscribe<PlayerScoredEvent>(OnPlayerScored);
            EventBus.Instance.Unsubscribe<ItemCollectedEvent>(OnItemCollected);
        }

        void Start() {
            EventBus.Instance.Publish(new PlayerScoredEvent(1, 100));
            EventBus.Instance.Publish(new ItemCollectedEvent(1, "Golden Sword"));
        }

        void OnPlayerScored(PlayerScoredEvent evt) {
            Debug.Log($"Player {evt.PlayerID} scored {evt.Points} points at {evt.Timestamp}");
        }

        void OnItemCollected(ItemCollectedEvent evt) {
            Debug.Log($"Player {evt.PlayerID} collected {evt.ItemName} at {evt.Timestamp}");
        }
    }
}