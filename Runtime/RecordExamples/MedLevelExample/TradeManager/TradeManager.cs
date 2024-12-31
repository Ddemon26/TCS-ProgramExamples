using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TCS {
    public class TradeManager : MonoBehaviour {
        readonly List<Item> m_playerInventory = new();
        readonly List<TradeTransaction> m_transactionHistory = new();

        void Start() {
            // Initialize player inventory with a sample item
            m_playerInventory.Add(new Item("Health Potion", 1, 50));
            m_playerInventory.Add(new Item("Magic Scroll", 2, 120));

            // Example trade offer from an NPC
            var npcTradeOffer = new TradeOffer
            (
                OfferedItem: new Item("Sword", 3, 200),
                RequestedItem: new Item("Magic Scroll", 2, 120),
                TraderName: "NPC Merchant"
            );

            AttemptTrade(npcTradeOffer);
            DisplayTransactionHistory();
        }

        // Attempt a trade based on the trade offer
        void AttemptTrade(TradeOffer offer) {
            var playerItem = m_playerInventory.FirstOrDefault(item => item.ID == offer.RequestedItem.ID);

            if (playerItem != null) {
                // Player has the item, proceed with trade
                m_playerInventory.Remove(playerItem);
                m_playerInventory.Add(offer.OfferedItem);

                // Log transaction
                var transaction = new TradeTransaction
                (
                    PlayerName: "Player1",
                    NpcName: offer.TraderName,
                    PlayerItem: playerItem,
                    NpcItem: offer.OfferedItem,
                    TransactionTime: DateTime.Now
                );
                m_transactionHistory.Add(transaction);

                Debug.Log($"Trade completed! Received {offer.OfferedItem.Name} for {playerItem.Name} from {offer.TraderName}.");
            }
            else {
                Debug.Log($"Trade failed. Player does not have the requested item: {offer.RequestedItem.Name}");
            }
        }

        // Display transaction history
        void DisplayTransactionHistory() {
            Debug.Log("Transaction History:");
            foreach (var transaction in m_transactionHistory)
            {
                Debug.Log($"Trade with {transaction.NpcName}: " +
                          $"Player gave {transaction.PlayerItem.Name}, " +
                          $"received {transaction.NpcItem.Name}, " +
                          $"on {transaction.TransactionTime}");
            }
            Debug.Log($"Total transactions recorded: {m_transactionHistory.Count}");
        }
    }
}