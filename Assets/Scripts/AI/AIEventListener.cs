using UnityEngine;
using UnityEngine.Events;

namespace AISystem
{
    public class AIEventListener : MonoBehaviour
    {
        public AIEvent AiEvent;
        public UnityEvent Response;

        private void OnEnable()
        {
            AiEvent.Register(this);
        }

        private void OnDisable()
        {
            AiEvent.Unregister(this);
        }

        public void OnEventRaised()
        {
            Response?.Invoke();
        }
    }
}


