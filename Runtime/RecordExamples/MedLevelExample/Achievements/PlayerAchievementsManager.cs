using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TCS {
    public class PlayerAchievementsManager : MonoBehaviour {
        readonly List<Achievement> m_achievements = new();

        void Start() {
            // Initialize achievements
            m_achievements.Add(new Achievement { Title = "First Blood", Description = "Complete your first mission" });
            m_achievements.Add(new Achievement { Title = "Explorer", Description = "Discover 5 new locations" });
            m_achievements.Add(new Achievement { Title = "Collector", Description = "Collect 100 items" });

            // Display initial achievements
            Debug.Log("Achievements Initialized:");
            DisplayAchievements();
        }

        // Method to unlock an achievement based on its title
        void UnlockAchievement(string title) {
            var achievement = m_achievements.FirstOrDefault(a => a.Title == title && !a.IsUnlocked);
            if (achievement != null) {
                m_achievements[m_achievements.IndexOf(achievement)] = achievement.Unlock();
                Debug.Log($"Achievement Unlocked: {achievement.Title}");
            }
            else {
                Debug.Log($"Achievement \"{title}\" is already unlocked or doesn't exist.");
            }
        }

        // Display the current state of all achievements
        void DisplayAchievements() {
            foreach (var achievement in m_achievements) {
                string status = achievement.IsUnlocked
                    ? $"Unlocked on {achievement.UnlockDate}"
                    : "Locked";
                Debug.Log($"Achievement: {achievement.Title}, Status: {status}, Description: {achievement.Description}");
            }
        }

        void Update() {
            // Example runtime action: Unlock "First Blood" by pressing 'F'
            if (Input.GetKeyDown(KeyCode.F)) {
                UnlockAchievement("First Blood");
                DisplayAchievements();
            }

            // Unlock "Explorer" by pressing 'E'
            if (Input.GetKeyDown(KeyCode.E)) {
                UnlockAchievement("Explorer");
                DisplayAchievements();
            }

            // Unlock "Collector" by pressing 'C'
            if (Input.GetKeyDown(KeyCode.C)) {
                UnlockAchievement("Collector");
                DisplayAchievements();
            }
        }
    }
}