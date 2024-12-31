using UnityEngine;
namespace TCS.Tests.RecordExamples {
    public class PlayerRecord : MonoBehaviour {
        PlayerStats m_initialStats;
        PlayerStats m_updatedStats;
    
        void Start()
        {
            m_initialStats = new PlayerStats(100, 50, 20);

            Debug.Log($"Initial Stats: {m_initialStats}");

            m_updatedStats = m_initialStats with { Health = m_initialStats.Health - 10 };
            m_updatedStats = m_updatedStats with { Mana = m_updatedStats.Mana + 10 };
            m_updatedStats = m_updatedStats with { Strength = m_updatedStats.Strength * 2 };
        
            Debug.Log($"Updated Stats: {m_updatedStats}");
        }
    }
}