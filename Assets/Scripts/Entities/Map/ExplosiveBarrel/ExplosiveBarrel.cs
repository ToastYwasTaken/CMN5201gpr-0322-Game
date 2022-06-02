using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBarrel : EntityStats
{
    [Header("Explosion Effect")]
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _explosionObject;
    
    private AudioManager _audioManager;

    private void Awake()
    {
        if (_audioManager == null) _audioManager = GameObject.Find("/AudioManager")?.GetComponent<AudioManager>();
    }

    protected override void Death()
    {
        ProjectileStats projectileStats = SetupProjectileStats(this, _audioManager);
        SpawnExplosion(projectileStats, this.gameObject);
    }

    private void SpawnExplosion(ProjectileStats projectileStats, GameObject projectile)
    {
        GameObject explosionGO = Instantiate(_explosionObject, projectile.transform);
        if (explosionGO == null) return;
        explosionGO.transform.SetParent(null);

        Explosion explosion = explosionGO.GetComponent<Explosion>();
        if (explosion == null) return;

        explosion.ProjectileStats = projectileStats;
        explosion.ExplosionRadius = _explosionRadius;

        Destroy(projectile);
    }

    private ProjectileStats SetupProjectileStats(EntityStats playerStats, AudioManager audioManager)
    {
        ProjectileStats newBulletStats = new()
        {
            ArmorPenetration = ArmorPenetration,
            AttackPower = DamageCalculation.CalculateAttackPower(playerStats.BaseAttackPower, 0f),

            CanCrit = playerStats.CanCrit,
            CritChance = playerStats.CritChanceNormalized,

            ProjectileOwnerType = playerStats.EntityType,
            ProjectileSender = playerStats.gameObject,

            WeaponAudio = SetupWeaponAudio(audioManager),

            ProjectileSpeed = 0f,
            ProjectileLifeTime = 0f
        };
        return newBulletStats;
    }

    private WeaponAudio SetupWeaponAudio(AudioManager audioManager)
    {
        WeaponAudio weaponAudio = new()
        {
            WeaponImpactSound = null,
            WeaponShootSound = null,
            AudioManager = audioManager,
        };
        return weaponAudio;
    }
}
