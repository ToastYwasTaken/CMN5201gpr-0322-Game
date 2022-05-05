using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapons/Guns", order = 100)]
public class Weapon : Item
{
    [SerializeField] protected float _fireRate;
    public float FireRate { get => _fireRate; set => _fireRate = value; }

    private float _currentCooldown;
    public float CurrentCooldown
    {
        get => _currentCooldown;
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

    [SerializeField] protected ShootBehaviour _shootBehaviour = null;
    public ShootBehaviour ShootBehaviour { get => _shootBehaviour; }

    [SerializeField] private int _amountOfBullets;
    [SerializeField] private float _fireAngle;
    [SerializeField] private bool _randomAngle = false;


    public virtual void OnEquip() { }

    public virtual void OnUnequip() { }


    public virtual void Shoot(Heatmeter heatmeter, bool useHeatmeter, EntityStats playerStats, GameObject firePoint)
    {
        if (_isOnCooldown) return;

        if (useHeatmeter)
        {
            if (heatmeter == null) return;
            if (heatmeter.IsOverheated) return;
            heatmeter.AddHeatlevel(_heatmeterUsage);
        }

        _isOnCooldown = true;
        CurrentCooldown = _fireRate;

        ProjectileStats newBulletStats = new();

        newBulletStats.ArmorPenetration = _armorPenetration;
        newBulletStats.AttackPower = playerStats.AttackPower * _weaponPower;
        newBulletStats.ProjectileSpeed = _bulletSpeed;
        newBulletStats.ProjectileLifeTime = 100f;

        if (_shootBehaviour != null)
            _shootBehaviour.Fire(newBulletStats, _bulletPrefab, firePoint, _amountOfBullets, _fireAngle, _randomAngle);
    }
}
