using System.Collections.Generic;
using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIEvent.cs
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
    /// Define event for the AI states
    /// </summary>
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

        /// <summary>
        /// Call the event
        /// </summary>
        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].OnEventRaised();
            }
        }

    }
}


