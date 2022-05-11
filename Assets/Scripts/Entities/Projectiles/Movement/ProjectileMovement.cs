using UnityEngine;

public abstract class ProjectileMovement : ScriptableObject
{
    public virtual Vector3 MovementVector(float speed, Transform transform)
    {
        return Vector3.zero;
    }
}
