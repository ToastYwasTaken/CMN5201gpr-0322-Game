using UnityEngine;

public class WeaponSlot
{
    private float _currentCooldown;
    public float CurrentCooldown
    {
        get => _currentCooldown;
        set
        {
            if (value < 0) value = 0;
            if (value > _weaponItem.FireRate) value = _weaponItem.FireRate;

            if (value > 0) _isOnCooldown = true;
            if (value == 0) _isOnCooldown = false;

            _currentCooldown = value;
        }
    }

    protected bool _isOnCooldown;
    public bool IsOnCooldown { get => _isOnCooldown; }

    [SerializeField] private Weapon _weaponItem;

    public WeaponSlot(Weapon weaponItem) => WeaponItem=weaponItem;

    public Weapon WeaponItem
    {
        get => _weaponItem;
        set
        {
            if (value == null) return;

            if (_weaponItem != null) _weaponItem.OnUnequip();

            _weaponItem = value;

            if (_weaponItem != null) _weaponItem.OnEquip();
        }
    }

    public void Shoot(Heatmeter heatmeter, bool useHeatmeter, EntityStats playerStats, GameObject firePoint)
    {
        if (_isOnCooldown) return;

        if (useHeatmeter)
        {
            if (heatmeter == null) return;
            if (heatmeter.IsOverheated) return;
            heatmeter.AddHeatlevel(_weaponItem.HeatmeterUsage);
        }

        _isOnCooldown = true;
        CurrentCooldown = _weaponItem.FireRate;

        _weaponItem.Shoot(playerStats, firePoint);
    }
}
