using UnityEngine;

namespace TCS {
    public class GameDifficultySettingsExample : MonoBehaviour {
        record GameDifficultySettings(int EnemySpawnRate, int MaxEnemies, float PlayerDamageMultiplier);

        readonly GameDifficultySettings m_easySettings = new(5, 10, 1.2f);
        GameDifficultySettings m_hardSettings = new(10, 20, 1.5f);

        void Start() {
            Debug.Log($"Easy Settings: EnemySpawnRate={m_easySettings.EnemySpawnRate}, MaxEnemies={m_easySettings.MaxEnemies}, PlayerDamageMultiplier={m_easySettings.PlayerDamageMultiplier}");
        }
    }
}