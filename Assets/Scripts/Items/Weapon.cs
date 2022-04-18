using Assets.Scripts.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Guns", order = 100)]
public class Weapon : Item
{
    [SerializeField] protected float _fireRate;
    public float FireRate { get => _fireRate; set => _fireRate = value; }

    private float _currentCooldown;
    public float CurrentCooldown 
    { get => _currentCooldown; 
      set
        {
            if (value < 0) value = 0;
            if (value > _fireRate) value = _fireRate;

            if (value == 0) _isOnCooldown = false;

            _currentCooldown = value;
        }
    }

    protected bool _isOnCooldown;
    public bool IsOnCooldown { get => _isOnCooldown; }

    [SerializeField] protected GameObject _bulletPrefab;
    public GameObject BulletPrefab { get => _bulletPrefab; set => _bulletPrefab = value; }

    [SerializeField] protected float _weaponPower;
    public float WeaponPower { get => _weaponPower; set => _weaponPower = value; }
    [SerializeField] protected float _armorPenetration;
    public float ArmorPenetraion { get => _armorPenetration; set => _armorPenetration = value; }
    [SerializeField] protected float _bulletSpeed;
    public float BulletSpeed { get => _bulletSpeed; set => _bulletSpeed = value; }
    [SerializeField] protected float _heatmeterUsage;
    public float HeatmeterUsage { get => _heatmeterUsage; set => _heatmeterUsage = value; }

    protected Heatmeter _heatmeter;    

    private void SetHeatmeter(Heatmeter heatmeter)
    {
        _heatmeter = heatmeter;
    }

    public void Shoot(Heatmeter heatmeter, PlayerStats playerStats, GameObject firePoint)
    {
        if (_isOnCooldown) return;
        if (heatmeter == null) return;

        SetHeatmeter(heatmeter);

        if (_heatmeter == null) return;
        if (_heatmeter.IsOverheated) return;

        _heatmeter.AddHeatlevel(_heatmeterUsage);
        _isOnCooldown = true;
        CurrentCooldown = _fireRate;

        ProjectileStats newBulletStats = new();

        newBulletStats.ArmorPenetration = _armorPenetration;
        newBulletStats.AttackPower = playerStats.AttackPower * _weaponPower;
        newBulletStats.ProjectileSpeed = _bulletSpeed;
        newBulletStats.ProjectileLifeTime = 100f;

        GameObject newBullet = Instantiate(_bulletPrefab, firePoint.transform);
        newBullet.GetComponent<Projectile>().ProjectileStats = newBulletStats;
    }
}
