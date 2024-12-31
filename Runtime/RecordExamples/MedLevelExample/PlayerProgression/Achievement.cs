namespace TCS {
    public record Achievement {
        public string Title { get; init; }
        public string Description { get; init; }
        public bool IsUnlocked { get; init; }

        public Achievement Unlock() {
            return this with { IsUnlocked = true };
        }
    }
}