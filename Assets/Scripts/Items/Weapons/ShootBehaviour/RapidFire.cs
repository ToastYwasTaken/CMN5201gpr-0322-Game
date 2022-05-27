using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Rapidfire", order = 100)]
public class RapidFire : ShotBehaviour
{

    [SerializeField] private int _amountOfBullets;
    [SerializeField] private float _delayBetweenShots;

    public override async void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent, WeaponAudio weaponManager)
    {
        for (int i = 0; i < _amountOfBullets; i++)
        {            
            GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
            PlaySound(weaponManager);

            newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;
            newBullet.transform.SetParent(parent);

            await Task.Delay((int)(_delayBetweenShots*1000f));            
        }        
    }
}

