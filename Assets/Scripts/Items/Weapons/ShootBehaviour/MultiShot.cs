using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Multishot", order = 100)]
public class MultiShot : ShootBehaviour
{
    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, int amountOfBullets, float angle, bool randomAngle)
    {
        float facingRotation = firePoint.transform.rotation.z;
        float startRoation = (facingRotation + angle) /2f;
        float angleIncrease = angle / ((float)amountOfBullets -1);

        for (int i = 0; i < amountOfBullets; i++)
        {
            //Quaternion tempTarget = new Quaternion(0f, 0f, (startRoation + angleIncrease) * i, 0);

            //Quaternion.

            GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);
            newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

            newBullet.transform.SetParent(null);
        }
    }
}
