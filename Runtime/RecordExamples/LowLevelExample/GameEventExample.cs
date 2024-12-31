using System;
using UnityEngine;
namespace TCS {
    public class GameEventExample : MonoBehaviour {
        enum EventType { ItemPickup, BossFight, ExperienceGain }
        record GameEvent(int EventID, DateTime Timestamp, EventType Type, string Details);

        readonly GameEvent m_bossFightStarted = new(1, DateTime.Now, EventType.BossFight, "The player encountered the Dragon Boss");

        void Start() {
            Debug.Log($"Game Event: Type={m_bossFightStarted.Type}, Details={m_bossFightStarted.Details}");
        }
    }
}