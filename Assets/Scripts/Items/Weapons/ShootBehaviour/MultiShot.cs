using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Multishot", order = 100)]
public class MultiShot : ShootBehaviour
{
    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent, int amountOfBullets, float angle, bool randomAngle)
    {
        if (randomAngle) 
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);
                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + angle /2f;
                float angleIncrease = Random.Range(0f, angle);

                float tempRot = startRoation - angleIncrease;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot));

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(parent);
            }
        } 
        else
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);
                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + angle /2f;
                float angleIncrease = angle / ((float)amountOfBullets -1);

                float tempRot = startRoation - angleIncrease * i;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot)); 

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(parent);
            }
        }
    }
}