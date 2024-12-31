namespace TCS {
    public record PlayerScoredEvent(int PlayerID, int Points) : GameEvent;
}