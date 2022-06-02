using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Rapidfire", order = 100)]
public class RapidFire : ShotBehaviour
{

    [SerializeField] private int _amountOfBullets;
    [SerializeField] private float _delayBetweenShots;

    [SerializeField] private bool _randomBulletAmount = false;
    [SerializeField] private float _maxRandomBulletAmount;

    public override async void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent,
                                    WeaponAudio weaponAudio, WeaponScreenshake weaponScreenshake)
    {
        float bulletAmount = _amountOfBullets;

        if (_randomBulletAmount)
        {
            if (_maxRandomBulletAmount < _amountOfBullets) return;
            else bulletAmount = Random.Range(_amountOfBullets, _maxRandomBulletAmount);
        }

        for (int i = 0; i < bulletAmount; i++)
        {            
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            PlaySound(weaponAudio);
            Screenshake(weaponScreenshake);

            newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;
            newBullet.transform.SetParent(parent);

            await Task.Delay((int)(_delayBetweenShots*1000f));            
        }        
    }
}

