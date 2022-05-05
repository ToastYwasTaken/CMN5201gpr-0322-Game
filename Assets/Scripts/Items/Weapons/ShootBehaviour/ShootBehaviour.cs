using Assets.Scripts.Player;
using UnityEngine;

public abstract class ShootBehaviour : ScriptableObject
{
    public virtual void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, int amountOfBullets, float angle, bool randomAngle)
    {

    }
}
