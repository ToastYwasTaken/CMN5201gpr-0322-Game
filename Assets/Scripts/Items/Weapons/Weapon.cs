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
        ProjectileStats newBulletStats = SetupProjectileStats(playerStats);

        if (_shootBehaviour != null)
            _shootBehaviour.Fire(newBulletStats, _bulletPrefab, firePoint, parent);
    }

    private ProjectileStats SetupProjectileStats(EntityStats playerStats)
    {
        ProjectileStats newBulletStats = new()
        {
            ArmorPenetration = _armorPenetration,
            AttackPower = CalculateAttackPower(playerStats.AttackPower, _weaponPower),

            CanCrit = playerStats.CanCrit,
            CritChance = playerStats.CritChance,

            ProjectileOwnerType = playerStats.EntityType,
            ProjectileSender = playerStats.gameObject,

            ProjectileSpeed = _bulletSpeed,
            ProjectileLifeTime = 20f
        };
        return newBulletStats;
    }

    public float CalculateAttackPower(float attackPower, float weaponPower)
    {
        return attackPower + weaponPower;
    }
}