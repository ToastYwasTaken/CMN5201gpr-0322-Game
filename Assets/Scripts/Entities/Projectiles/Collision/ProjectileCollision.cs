using UnityEngine;

public abstract class ProjectileCollision : ScriptableObject
{
    public virtual void OnCollision(Collider collision, ProjectileStats projectileStats, GameObject projectile)
    {

    }
}
