using System.Collections.Generic;
using UnityEngine;

namespace TCS {
    public class SocialSystemMono : MonoBehaviour {
        SocialManager m_socialManager;

        void Start() {
            m_socialManager = new SocialManager();

            // Example usage: Sending friend requests
            m_socialManager.SendFriendRequest("Player1", "Player2");
            m_socialManager.SendFriendRequest("Player2", "Player3");

            // Example usage: Sending a message
            m_socialManager.SendMessage("Player1", "Player2", "Hello, want to team up?");

            // Example usage: Updating player status
            m_socialManager.UpdatePlayerStatus("Player1", PlayerStatus.OnlineStatus.Online, "Lobby");

            // Print initial friend requests for Player2
            PrintFriendRequests("Player2");

            // Accept a friend request
            IEnumerable<FriendRequest> friendRequests = m_socialManager.GetFriendRequestsForPlayer("Player2");
            foreach (var request in friendRequests) {
                m_socialManager.RespondToFriendRequest(request, true);
            }

            // Print messages for Player2
            PrintMessages("Player2");

            // Print updated friend requests for Player2
            PrintFriendRequests("Player2");
        }

        void PrintFriendRequests(string playerId) {
            IEnumerable<FriendRequest> requests = m_socialManager.GetFriendRequestsForPlayer(playerId);
            foreach (var request in requests) {
                Debug.Log($"Friend request from {request.SenderId} to {request.RecipientId} - Status: {request.Status}");
            }
        }

        void PrintMessages(string playerId) {
            IEnumerable<Message> messages = m_socialManager.GetMessagesForPlayer(playerId);
            foreach (var message in messages) {
                Debug.Log($"Message from {message.SenderId} to {message.RecipientId}: {message.Content} at {message.Timestamp}");
            }
        }
    }
}