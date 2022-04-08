using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace AISystem
{
    public class AIEventListener : MonoBehaviour
    {
        public AIEvent aiEvent;
        public UnityEvent response;

        private void OnEnable()
        {
            aiEvent.Register(this);
        }

        private void OnDisable()
        {
            aiEvent.Unregister(this);
        }

        public void OnEventRaised()
        {
            response?.Invoke();
        }
    }
}


