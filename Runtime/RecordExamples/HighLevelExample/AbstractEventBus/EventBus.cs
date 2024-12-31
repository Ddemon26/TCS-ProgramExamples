using System;
using System.Collections.Generic;
namespace TCS {
    public class EventBus {
        readonly Dictionary<Type, List<Action<GameEvent>>> m_eventListeners = new();
        static EventBus instance;
        public static EventBus Instance => instance ??= new EventBus();

        public void Subscribe<T>(Action<T> listener) where T : GameEvent {
            if (!m_eventListeners.ContainsKey(typeof(T))) {
                m_eventListeners[typeof(T)] = new List<Action<GameEvent>>();
            }

            m_eventListeners[typeof(T)].Add(e => listener((T)e));
        }

        public void Unsubscribe<T>(Action<T> listener) where T : GameEvent {
            if (m_eventListeners.ContainsKey(typeof(T))) {
                m_eventListeners[typeof(T)].Remove(e => listener((T)e));
            }
        }

        public void Publish<T>(T gameEvent) where T : GameEvent {
            if (!m_eventListeners.TryGetValue(typeof(T), out List<Action<GameEvent>> listeners)) return;
            foreach (Action<GameEvent> listener in listeners) {
                listener(gameEvent);
            }
        }
    }
}