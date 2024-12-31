namespace TCS {
    public record FriendRequest {
        public string SenderId { get; init; }
        public string RecipientId { get; init; }
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public enum RequestStatus {
            Pending,
            Accepted,
            Rejected,
        }
    }
}