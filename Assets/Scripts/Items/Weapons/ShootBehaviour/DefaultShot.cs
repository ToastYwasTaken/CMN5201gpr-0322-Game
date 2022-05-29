using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Default", order = 100)]
public class DefaultShot : ShotBehaviour
{
    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent, 
                              WeaponAudio weaponAudio, WeaponScreenshake weaponScreenshake)
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        PlaySound(weaponAudio);
        Screenshake(weaponScreenshake);

        newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;
        newBullet.transform.SetParent(parent);
    }
}