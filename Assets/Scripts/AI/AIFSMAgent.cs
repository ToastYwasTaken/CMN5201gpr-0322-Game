using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIFSMAgent.cs
* Date : 09.04.2022
* Author : René Kraus (RK)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
******************************************************************************/
namespace AISystem
{
    /// <summary>
    /// Finite State Machine
    /// Initialize and controls the AI states
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent))]
    public class AIFSMAgent : MonoBehaviour
    {
        [SerializeField] private AIBaseState _initialState;
        [SerializeField] private bool _showDebugLogs = false;
        public string PlayerTag = "Player";

        private Dictionary<Type, Component> _cachedComponents;

        #region Propertys
        public GameObject Owner => this.gameObject;

        /// <summary>
        /// Initialize the State by changing
        /// </summary>
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

        /// <summary>
        /// Initialize the first state
        /// </summary>
        private void Awake()
        {
            _cachedComponents = new Dictionary<Type, Component>();
            CurrentState = _initialState;

        }

        /// <summary>
        /// Execute the current state 
        /// </summary>
        private void Update()
        {
            if (CurrentState != null)
                CurrentState.Execute(this);
            if (_showDebugLogs)
                Debug.Log($"AI: {gameObject.name} | Current State: {CurrentState.name}");
        }

        /// <summary>
        /// Initialize the state
        /// </summary>
        private void InitializeState()
        {
            if (_showDebugLogs)
                Debug.Log($"AI: {gameObject.name} | Initialize State: {CurrentState.name}");
            CurrentState.Initialize(this);
        }

        /// <summary>
        /// Leave the state
        /// </summary>
        /// <param name="initialState"></param>
        private void ExitState(AIBaseState initialState)
        {
            if (initialState == CurrentState || CurrentState == null) return;
            if (_showDebugLogs)
                Debug.Log($"AI: {gameObject.name} | Exit State: {CurrentState.name}");
            CurrentState.Exit(this);
        }

        /// <summary>
        /// Returns the components of the parent GameObject
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Component</returns>
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
        /// Sets a random position within the NavMesh
        /// </summary>
        /// <param name="range"></param>
        public void SetPositionRandomAtNavMesh(float range, float maxDistance)
        {
            if (PositionOnNavMesh(range, maxDistance, out Vector3 newPoint))
            {
                transform.position = newPoint;
            }
            else
            {
                // Destroy the enemy, when they are not on the NavMesh
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Returns whether the position is on the NavMesh
        /// </summary>
        /// <param name="range"></param>
        /// <param name="maxDistance"></param>
        /// <param name="result"></param>
        /// <returns>bool</returns>
        public bool PositionOnNavMesh(float range, float maxDistance, out Vector3 result)
        {
            Vector3 center = transform.position;
            Vector3 rndPoint = center + (Random.insideUnitSphere * range);

            for (int i = 0; i < 100; i++)
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