using System;
using UnityEngine;

public abstract class ProjectileCollision : ScriptableObject
{
    public abstract void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile);
}
