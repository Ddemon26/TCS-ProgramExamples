using UnityEngine;
namespace TCS {
    public class PlayerProfileExample : MonoBehaviour {
        record PlayerProfile(string PlayerName, int PlayerID, int Age);

        readonly PlayerProfile m_playerProfile = new("Hero123", 1001, 25);

        void Start() {
            Debug.Log($"Player Profile: Name={m_playerProfile.PlayerName}, ID={m_playerProfile.PlayerID}, Age={m_playerProfile.Age}");
        }
    }
}