using UnityEngine;

public abstract class ProjectileCollision : ScriptableObject
{
    public virtual void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {

    }
}
