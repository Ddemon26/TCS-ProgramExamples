using UnityEngine;
namespace TCS {
    public class SkillExample : MonoBehaviour {
        record Skill(string Name, int Damage, int Cost, float Cooldown, string Description);

        readonly Skill m_fireball = new("Fireball", 50, 20, 3.5f, "Throws a ball of fire that deals damage to enemies");
        Skill m_heal = new("Heal", 0, 10, 5.0f, "Restores health to the player");

        void Start() {
            Debug.Log($"Skill: {m_fireball.Name}, Damage={m_fireball.Damage}, Cooldown={m_fireball.Cooldown}");
        }
    }
}