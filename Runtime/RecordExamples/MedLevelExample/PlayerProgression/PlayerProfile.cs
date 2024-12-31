namespace TCS {
    public record PlayerProfile {
        public string PlayerName { get; init; }
        public int Level { get; init; }
        public int Experience { get; init; }
        public int Health { get; init; }

        public PlayerProfile LevelUp() {
            return this with { Level = this.Level + 1, Experience = 0 };
        }

        public PlayerProfile GainExperience(int amount) {
            int newExperience = Experience + amount;
            if (newExperience >= Level * 100) // Level-up threshold
            {
                return LevelUp();
            }

            return this with { Experience = newExperience };
        }
    }
}