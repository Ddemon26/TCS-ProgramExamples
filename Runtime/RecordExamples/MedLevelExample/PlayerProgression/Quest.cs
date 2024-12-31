namespace TCS {
    public record Quest {
        public string QuestName { get; init; }
        public string Objective { get; init; }
        public bool IsCompleted { get; init; }

        public Quest Complete() {
            return this with { IsCompleted = true };
        }
    }
}