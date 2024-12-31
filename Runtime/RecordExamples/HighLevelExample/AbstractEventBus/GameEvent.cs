using System;
namespace TCS {
    public abstract record GameEvent {
        public DateTime Timestamp { get; init; } = DateTime.Now;
    }
}