using UnityEngine;

namespace AISystem
{
    [CreateAssetMenu(menuName = "AI FSM/AI Configuration")]
    public class AIConfiguration : ScriptableObject
    {
        [Header("Agent Configuration")]
        public float speed = 3.5f;
        public float angularSpeed = 120.0f;
        public float acceleration = 3.5f;
        public float stoppingDistance = 3.5f;
        public bool autoBraking = true;

    }
}

