using System;
namespace TCS {
    public record Message {
        public string SenderId { get; init; }
        public string RecipientId { get; init; }
        public string Content { get; init; }
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }
}