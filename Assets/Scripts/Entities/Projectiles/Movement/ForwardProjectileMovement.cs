using UnityEngine;

[CreateAssetMenu(fileName = "New Forward Movement", menuName = "Entities/Projectiles/MovementBehaviour/Forward", order = 100)]
public class ForwardProjectileMovement : ProjectileMovement
{
    public override Vector3 MovementVector(float speed, Transform transform)
    {
        return speed*Time.deltaTime*transform.up;
    }
}
