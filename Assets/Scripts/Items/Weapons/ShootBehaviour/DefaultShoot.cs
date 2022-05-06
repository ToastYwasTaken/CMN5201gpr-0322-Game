using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Default", order = 100)]
public class DefaultShoot : ShootBehaviour
{
    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, int amountOfBullets, float angle, bool randomAngle)
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);
        newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

        newBullet.transform.SetParent(null); //
    }
}
