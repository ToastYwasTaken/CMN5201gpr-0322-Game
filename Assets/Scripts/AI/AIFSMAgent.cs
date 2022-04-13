using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AISystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIFSMAgent : MonoBehaviour
    {
        [SerializeField] private AIBaseState _initialState;

        private Dictionary<Type, Component> _cachedComponents;

        public string PlayerTag = "Player";

        #region Propertys

        private AIBaseState _currentState;
        public AIBaseState CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                if (_currentState != null)
                    _currentState.Initialize(this);
            }
        }

        #endregion

        private void Awake()
        {
            _cachedComponents = new Dictionary<Type, Component>();
            CurrentState = _initialState;

        }

        private void Update()
        {
            CurrentState.Execute(this);
            Debug.Log($"AI: {gameObject.name} | Current State: {CurrentState.name}");
        }

        public new T GetComponent<T>() where T : Component
        {
            if (_cachedComponents.ContainsKey(typeof(T)))
            {
                return _cachedComponents[typeof(T)] as T;
            }

            T component = base.GetComponent<T>();
            if (component != null)
            {
                _cachedComponents.Add(typeof(T), component);
            }

            return component;
        }
    }
}