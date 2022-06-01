using UnityEngine;

/*****************************************************************************
* Project: CMN5201GPR-0322-Game
* File : AIConfiguration.cs
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
    /// Configuration Data for AI State
    /// </summary>
    [CreateAssetMenu(menuName = "AI FSM/AI Configuration")]
    public class AIConfiguration : ScriptableObject
    {
        [Header("Agent Configuration")]
        public float Speed = 3.5f;
        public float AngularSpeed = 120.0f;
        public float Acceleration = 3.5f;
        public float StoppingDistance = 3.5f;
        public bool AutoBraking = true;

    }
}

