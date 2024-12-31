using UnityEngine;
namespace TCS {
    public class AISettingsExample : MonoBehaviour {
        record AISettings(float PatrolRange, float DetectionRadius, int AggressivenessLevel);

        readonly AISettings m_guardAISettings = new(10.0f, 5.0f, 3);
        AISettings m_bossAISettings = new(20.0f, 10.0f, 5);

        void Start() {
            Debug.Log($"AI Settings: PatrolRange={m_guardAISettings.PatrolRange}, DetectionRadius={m_guardAISettings.DetectionRadius}, Aggressiveness={m_guardAISettings.AggressivenessLevel}");
        }
    }
}