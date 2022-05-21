using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AISystem
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIFSMAgent : MonoBehaviour
    {
        [SerializeField] private AIBaseState _initialState;
        [SerializeField] private bool _showDebugLogs = false;
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
            if (CurrentState != null)
                CurrentState.Execute(this);
            if (_showDebugLogs)
                Debug.Log($"AI: {gameObject.name} | Current State: {CurrentState.name}");
        }

        private void InitializeState()
        {
            if (_showDebugLogs)
                Debug.Log($"AI: {gameObject.name} | Initialize State: {CurrentState.name}");
            CurrentState.Initialize(this);
        }

        private void ExitState(AIBaseState initialState)
        {
            if (initialState == CurrentState || CurrentState == null) return;
            if (_showDebugLogs)
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

        /// <summary>
        /// Setzt eine zufällige Position innerhalb des NavMesh
        /// </summary>
        /// <param name="range"></param>
        public void SetPositionRandomAtNavMesh(float range, float maxDistance)
        {
            if (PositionOnNavMesh(range, maxDistance, out Vector3 newPoint))
            {
                transform.position = newPoint;
            }
            // else
            // {
            //     // Destory the enemy, when they are not on the NavMesh
            //     Destroy(gameObject);
            // }
        }

        public bool PositionOnNavMesh(float range, float maxDistance, out Vector3 result)
        {
            //var center = new Vector3(_navMeshCenter.x, _navMeshCenter.x, 0f);

            Vector3 center = transform.position;
            Vector3 rndPoint = center + (Random.insideUnitSphere * range);

            for (int i = 0; i < 50; i++)
            {
                if (!NavMesh.SamplePosition(rndPoint, out NavMeshHit hit, maxDistance, NavMesh.AllAreas)) continue;
                result = hit.position;
                return true;
            }
            result = Vector3.zero;
            return false;
        }
    }
}