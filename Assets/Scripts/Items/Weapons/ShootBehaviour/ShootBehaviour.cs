using Assets.Scripts.Player;
using UnityEngine;

public abstract class ShootBehaviour : ScriptableObject
{
    public virtual void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint,GameObject parent, int amountOfBullets, float angle, bool randomAngle)
    {

    }
}
