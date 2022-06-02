using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Multishot", order = 100)]
public class MultiShot : ShotBehaviour
{

    [SerializeField] private float _amountOfBullets;
    [SerializeField] private float _angle;
    [SerializeField] private bool _randomAngle;

    [SerializeField] private bool _randomBulletAmount = false;
    [SerializeField] private float _maxRandomBulletAmount;

    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent,
                              WeaponAudio weaponAudio, WeaponScreenshake weaponScreenshake)
    {
        float bulletAmount = _amountOfBullets;

        if (_randomBulletAmount)
        {
            if (_maxRandomBulletAmount < _amountOfBullets) return;
            else bulletAmount = Random.Range(_amountOfBullets, _maxRandomBulletAmount);
        }


        if (_randomAngle) 
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);

                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + _angle /2f;
                float angleIncrease = Random.Range(0f, _angle);

                float tempRot = startRoation - angleIncrease;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot));

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(parent);
                newBullet.transform.localScale = bulletPrefab.transform.localScale;
            }
        } 
        else
        {
            for (int i = 0; i < bulletAmount; i++)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform);
                PlaySound(weaponAudio);

                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + _angle /2f;
                float angleIncrease = _angle / ((float)bulletAmount -1);

                float tempRot = startRoation - angleIncrease * i;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot)); 

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(parent);
                newBullet.transform.localScale = bulletPrefab.transform.localScale;
            }
        }

        PlaySound(weaponAudio);
        Screenshake(weaponScreenshake);
    }
}