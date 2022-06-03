/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Weapon.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Guns", order = 100)]
public class Weapon : Item
{
    [Header("Weapon Stats")]
    [SerializeField] protected float _fireRate;
    public float FireRate { get => _fireRate; set => _fireRate = value; }

    [SerializeField] protected float _weaponPower;
    public float WeaponPower { get => _weaponPower; }

    [SerializeField] protected float _armorPenetration;
    public float ArmorPenetraion { get => _armorPenetration; }

    [SerializeField] protected float _bulletSpeed;
    public float BulletSpeed { get => _bulletSpeed; }

    [SerializeField] protected float _heatmeterUsage;
    public float HeatmeterUsage { get => _heatmeterUsage; }

    [Header("Bullets")]
    [SerializeField] protected GameObject _bulletPrefab = null;
    public GameObject BulletPrefab { get => _bulletPrefab; }

    [Header("Behaviours")]
    [SerializeField] protected ShotBehaviour _shootBehaviour = null;
    public ShotBehaviour ShootBehaviour { get => _shootBehaviour; }

    [Header("Audio")]
    [SerializeField] private AudioClip _weaponShootSound;
    [SerializeField] private AudioClip _weaponImpactSound;

    [Header("Screenshake")]
    [SerializeField] private bool _useScreenshake = false;
    [SerializeField] private ShakeData _shakeData = null;

    public virtual void OnEquip() { }

    public virtual void OnUnequip() { }

    public virtual void Shoot(EntityStats playerStats, GameObject firePoint, Transform parent, AudioManager audioManager)
    {
        if (_shootBehaviour != null)
        {
            _shootBehaviour.Fire(SetupProjectileStats(playerStats, audioManager),
                                 _bulletPrefab, firePoint, parent,
                                 SetupWeaponAudio(audioManager),
                                 SetupWeaponScreenshake());
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
            AttackPower = DamageCalculation.CalculateAttackPower(playerStats.BaseAttackPower, _weaponPower),

            CanCrit = playerStats.CanCrit,
            CritChance = playerStats.CritChanceNormalized,

            ProjectileOwnerType = playerStats.EntityType,
            ProjectileSender = playerStats.gameObject,

            WeaponAudio = SetupWeaponAudio(audioManager),

            ProjectileSpeed = _bulletSpeed,
            ProjectileLifeTime = 20f
        };
        return newBulletStats;
    }

    private WeaponScreenshake SetupWeaponScreenshake() 
    {
        WeaponScreenshake weaponScreenshake = new()
        {
            UseScreenshake = _useScreenshake,
            ShakeData = _shakeData,
        };
        return weaponScreenshake;
    }
}
