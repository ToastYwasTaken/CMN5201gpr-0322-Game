using System;
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

    [Header("Audio")]
    [SerializeField] private AudioClip _weaponShootSound;
    [SerializeField] private AudioClip _weaponImpactSound;

    public virtual void OnEquip() { }

    public virtual void OnUnequip() { }


    public virtual void Shoot(EntityStats playerStats, GameObject firePoint, Transform parent, AudioManager audioManager)
    {
        ProjectileStats newBulletStats = SetupProjectileStats(playerStats, audioManager);

        if (_shootBehaviour != null)
        {
            _shootBehaviour.Fire(newBulletStats, _bulletPrefab, firePoint, parent, SetupWeaponAudio(audioManager));            
        }
    }

    private WeaponAudio SetupWeaponAudio(AudioManager audioManager) 
    {
        WeaponAudio weaponAudio = new()
        {
            WeaponImpactSound = _weaponImpactSound,
            WeaponShootSound = _weaponShootSound,
            AudioManager = audioManager,
        };
        return weaponAudio;
    }

    private ProjectileStats SetupProjectileStats(EntityStats playerStats, AudioManager audioManager)
    {
        ProjectileStats newBulletStats = new()
        {
            ArmorPenetration = _armorPenetration,
            AttackPower = DamageCalculation.CalculateAttackPower(playerStats.AttackPower, _weaponPower),

            CanCrit = playerStats.CanCrit,
            CritChance = playerStats.CritChance,

            ProjectileOwnerType = playerStats.EntityType,
            ProjectileSender = playerStats.gameObject,

            WeaponAudio = SetupWeaponAudio(audioManager),

            ProjectileSpeed = _bulletSpeed,
            ProjectileLifeTime = 20f
        };
        return newBulletStats;
    }
}
