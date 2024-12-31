namespace TCS {
    public record PlayerStatus {
        public string PlayerId { get; init; }
        public OnlineStatus Status { get; set; } = OnlineStatus.Offline;
        public string CurrentGameMode { get; set; } = "Idle";

        public enum OnlineStatus {
            Online,
            Offline,
            InGame,
            Away,
        }
    }
}