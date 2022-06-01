using UnityEngine;
using UnityEngine.Events;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIEventListener.cs
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
    /// Event listener for AI events
    /// </summary>
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


