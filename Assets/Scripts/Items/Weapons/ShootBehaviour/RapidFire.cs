using UnityEngine;

public class RapidFire : ShotBehaviour
{
    [SerializeField] private int _amountOfBullets;
    [SerializeField] private float _delayBetweenShots;

    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent)
    {
        int shotsLeft = _amountOfBullets;
        float currentDelay = 0f;

        while (shotsLeft > 0)
        {
            Debug.LogWarning(currentDelay);
            if (currentDelay < 0) 
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;
                newBullet.transform.SetParent(parent);

                shotsLeft--;
                currentDelay = _delayBetweenShots;
                Debug.LogWarning("Bullet Fired");
            } 
            else currentDelay -= Time.deltaTime;
        }
    }
}

