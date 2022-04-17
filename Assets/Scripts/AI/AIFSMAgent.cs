using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Object = UnityEngine.Object;

namespace AISystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIFSMAgent : MonoBehaviour
    {
        [SerializeField] private AIBaseState _initialState;
        public string PlayerTag = "Player";
        
        private Dictionary<Type, Component> _cachedComponents;

        #region Propertys
        public GameObject Owner => this.gameObject;
        
        private AIBaseState _currentState;
        public AIBaseState CurrentState
        {
            get => _currentState;
            set
            {
                ExitState(value);
                _currentState = value;
                if (_currentState == null) return;
                InitializeState();
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
           // Debug.Log($"AI: {gameObject.name} | Current State: {CurrentState.name}");
        }

        private void InitializeState()
        {
            Debug.Log($"AI: {gameObject.name} | Initialize State: {CurrentState.name}");
            CurrentState.Initialize(this);
        }

        private void ExitState(AIBaseState initialState)
        {
            if (initialState == CurrentState || CurrentState == null) return;
            Debug.Log($"AI: {gameObject.name} | Exit State: {CurrentState.name}");
            CurrentState.Exit(this);
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