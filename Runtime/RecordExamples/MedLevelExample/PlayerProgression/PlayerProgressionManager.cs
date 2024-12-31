using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace TCS {
    public class PlayerProgressionManager : MonoBehaviour {
        public PlayerProfile Profile { get; private set; }
        public List<InventoryItem> Inventory { get; private set; }
        public List<Quest> Quests { get; private set; }
        public List<Achievement> Achievements { get; private set; }

        void Start() {
            Profile = new PlayerProfile { PlayerName = "Hero", Level = 1, Experience = 0, Health = 100 };
            Inventory = new List<InventoryItem>();
            Quests = new List<Quest>();
            Achievements = new List<Achievement>();

            // Add starting items, quests, and achievements
            AddItem(new InventoryItem { ItemName = "Health Potion", Description = "Restores health", Quantity = 1 });
            AddQuest(new Quest { QuestName = "First Steps", Objective = "Talk to the village elder" });
            AddAchievement(new Achievement { Title = "Beginner", Description = "Complete your first quest" });

            Debug.Log($"Profile initialized: {Profile}");
            Debug.Log($"Initial Inventory: {string.Join(", ", Inventory)}");
            Debug.Log($"Initial Quests: {string.Join(", ", Quests)}");
            Debug.Log($"Initial Achievements: {string.Join(", ", Achievements)}");
        }

        public void AddItem(InventoryItem item) {
            Debug.Log($"AddItem() called with item: {item}");

            var existingItem = Inventory.Find(i => i.ItemName == item.ItemName);
            if (existingItem != null) {
                Inventory.Remove(existingItem);
                Inventory.Add(existingItem.Add(item.Quantity));
                Debug.Log($"Updated existing item: {existingItem}");
            } else {
                Inventory.Add(item);
                Debug.Log($"Added new item: {item}");
            }

            Debug.Log($"Updated Inventory: {string.Join(", ", Inventory)}");
        }

        public void AddQuest(Quest quest) {
            Debug.Log($"AddQuest() called with quest: {quest}");
            Quests.Add(quest);
            Debug.Log($"Updated Quests: {string.Join(", ", Quests)}");
        }

        public void CompleteQuest(string questName) {
            Debug.Log($"CompleteQuest() called with questName: {questName}");

            var quest = Quests.Find(q => q.QuestName == questName);
            if (quest is not { IsCompleted: false }) {
                Debug.Log($"Quest not found or already completed: {questName}");
                return;
            }

            Quests.Remove(quest);
            Quests.Add(quest.Complete());
            Debug.Log($"Completed quest: {quest}");

            CheckAchievements();
        }

        public void GainExperience(int amount) {
            Debug.Log($"GainExperience() called with amount: {amount}");
            Profile = Profile.GainExperience(amount);
            Debug.Log($"Updated Profile: {Profile}");
        }

        public void AddAchievement(Achievement achievement) {
            Debug.Log($"AddAchievement() called with achievement: {achievement}");
            Achievements.Add(achievement);
            Debug.Log($"Updated Achievements: {string.Join(", ", Achievements)}");
        }

        public void CheckAchievements() {
            Debug.Log("CheckAchievements() called");

            foreach (var achievement in Achievements.Where(a => a.Title == "Beginner" && !a.IsUnlocked && Quests.Exists(q => q.IsCompleted)).ToList()) {
                Achievements.Remove(achievement);
                Achievements.Add(achievement.Unlock());
                Debug.Log($"Unlocked achievement: {achievement}");
            }

            Debug.Log($"Updated Achievements: {string.Join(", ", Achievements)}");
        }
    }
}