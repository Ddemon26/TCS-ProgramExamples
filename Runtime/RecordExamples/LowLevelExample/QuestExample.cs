using UnityEngine;
namespace TCS {
    public class QuestExample : MonoBehaviour {
        record Quest(string QuestName, string Objective, string Description, string Reward);

        readonly Quest m_fetchQuest = new("Gather Herbs", "Collect 10 medicinal herbs", "Help the healer by gathering herbs", "50 Gold");
        Quest m_rescueQuest = new("Rescue the Villager", "Save the villager from goblins", "The villager is trapped, and time is of the essence", "100 Gold and a Health Potion");

        void Start() {
            Debug.Log($"Quest: {m_fetchQuest.QuestName}, Objective={m_fetchQuest.Objective}, Reward={m_fetchQuest.Reward}");
        }
    }
}