using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Guns", order = 100)]
public class Weapon : Item
{
    [SerializeField] protected float _fireRate;
    public float FireRate { get => _fireRate; set => _fireRate = value; }



    [SerializeField] protected GameObject _bulletPrefab = null;
    public GameObject BulletPrefab { get => _bulletPrefab; }

    [SerializeField] protected float _weaponPower;
    public float WeaponPower { get => _weaponPower; }

    [SerializeField] protected float _armorPenetration;
    public float ArmorPenetraion { get => _armorPenetration; }

    [SerializeField] protected float _bulletSpeed;
    public float BulletSpeed { get => _bulletSpeed; }

    [SerializeField] protected float _heatmeterUsage;
    public float HeatmeterUsage { get => _heatmeterUsage; }

    [SerializeField] protected ShotBehaviour _shootBehaviour = null;
    public ShotBehaviour ShootBehaviour { get => _shootBehaviour; }
    
    public virtual void OnEquip() { }

    public virtual void OnUnequip() { }


    public virtual void Shoot(EntityStats playerStats, GameObject firePoint, Transform parent)
    {
        ProjectileStats newBulletStats = new();

        newBulletStats.ArmorPenetration = _armorPenetration;
        newBulletStats.AttackPower = CalculateAttackPower(playerStats.AttackPower, _weaponPower);
        newBulletStats.ProjectileSpeed = _bulletSpeed;
        newBulletStats.ProjectileLifeTime = 20f;
        newBulletStats.ProjectileSender = playerStats.gameObject;
        newBulletStats.ProjectileOwnerType = playerStats.EntityType;

        if (_shootBehaviour != null)
            _shootBehaviour.Fire(newBulletStats, _bulletPrefab, firePoint, parent);
    }

    public float CalculateAttackPower(float attackPower, float weaponPower)
    {
        return attackPower + weaponPower;
    }
}