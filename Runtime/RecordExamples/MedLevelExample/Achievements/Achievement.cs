namespace TCS {
    public record Achievement
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsUnlocked { get; init; }
        public System.DateTime? UnlockDate { get; init; }

        // Method to unlock the achievement
        public Achievement Unlock()
        {
            return this with { IsUnlocked = true, UnlockDate = System.DateTime.Now };
        }
    }
}