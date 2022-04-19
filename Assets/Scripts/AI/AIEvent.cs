using System.Collections.Generic;
using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(fileName = "OnAIEvent", menuName = "AI FSM/Events/OnEvent")]
    public class AIEvent : ScriptableObject
    {
        private readonly List<AIEventListener> _listeners = new();

        public void Register(AIEventListener listener)
        {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void Unregister(AIEventListener listener) => _listeners.Remove(listener);

        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

    }
}


