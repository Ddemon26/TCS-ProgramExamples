using System.Collections.Generic;
using System.Linq;
namespace TCS {
    public class SocialManager {
        readonly List<FriendRequest> m_friendRequests = new();
        readonly List<Message> m_messages = new();
        readonly Dictionary<string, PlayerStatus> m_playerStatuses = new();

        public void SendFriendRequest(string senderId, string recipientId) {
            var request = new FriendRequest { SenderId = senderId, RecipientId = recipientId };
            m_friendRequests.Add(request);
        }

        public void RespondToFriendRequest(FriendRequest request, bool accept) {
            request.Status = accept ? FriendRequest.RequestStatus.Accepted : FriendRequest.RequestStatus.Rejected;
        }

        public void SendMessage(string senderId, string recipientId, string content) {
            var message = new Message { SenderId = senderId, RecipientId = recipientId, Content = content };
            m_messages.Add(message);
        }

        public void UpdatePlayerStatus(string playerId, PlayerStatus.OnlineStatus status, string gameMode = "Idle") {
            if (m_playerStatuses.ContainsKey(playerId)) {
                m_playerStatuses[playerId].Status = status;
                m_playerStatuses[playerId].CurrentGameMode = gameMode;
            }
            else {
                m_playerStatuses[playerId] = new PlayerStatus { PlayerId = playerId, Status = status, CurrentGameMode = gameMode };
            }
        }

        public IEnumerable<Message> GetMessagesForPlayer(string playerId) {
            return m_messages.Where(m => m.RecipientId == playerId);
        }

        public IEnumerable<FriendRequest> GetFriendRequestsForPlayer(string playerId) {
            return m_friendRequests.Where(r => r.RecipientId == playerId && r.Status == FriendRequest.RequestStatus.Pending);
        }
    }
}