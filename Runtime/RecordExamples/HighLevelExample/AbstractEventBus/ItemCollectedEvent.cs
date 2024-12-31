namespace TCS {
    public record ItemCollectedEvent(int PlayerID, string ItemName) : GameEvent;
}