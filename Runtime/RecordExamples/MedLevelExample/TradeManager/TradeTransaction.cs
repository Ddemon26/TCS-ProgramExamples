using System;
namespace TCS {
    public record TradeTransaction(
        string PlayerName,
        string NpcName,
        Item PlayerItem,
        Item NpcItem,
        DateTime TransactionTime
    );
}