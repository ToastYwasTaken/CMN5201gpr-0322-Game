using UnityEngine;

public abstract class ShotBehaviour : ScriptableObject
{
    public virtual void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint,Transform parent)
    {

    }
}
