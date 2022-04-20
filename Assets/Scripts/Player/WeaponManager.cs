using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour, IWeapon
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Heatmeter _heatmeter;

    private Weapon[] _weapons;
    [SerializeField] private Weapon[] _weaponItems;
    [SerializeField] private Weapon _defaultWeapon;

    [SerializeField] private GameObject[] _firePoints;

    private readonly int _weaponSlotAmount = 2;

    private void Awake()
    {
        if(_weaponItems == null) _weaponItems = new Weapon[_weaponSlotAmount];
        _weapons = new Weapon[_weaponSlotAmount];

        if(_playerController == null)
        _playerController = GetComponent<PlayerController>();

        if (_heatmeter == null)
            _heatmeter = GetComponent<Heatmeter>();
    }

    private void Update()
    {
        ReduceWeaponsCooldown();
        LoadWeapons();
    }

    private void OnEnable()
    {
        InitializeWeapons();

        LoadWeapons();
    }

    private void LoadWeapons()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weaponItems == null) return;
            if (_weaponItems[i] == null) return;

            SetupWeapon(_weaponItems[i], i);
        }
    }

    private void InitializeWeapons()
    {
        for (int i = 0; i < _weapons.Length; i++)
        {
            SetupWeapon(_defaultWeapon, i);
        }
    }

    private void SetupWeapon(Weapon newWeapon, int weaponSlot)
    {
        if (newWeapon == null) return;
        if (weaponSlot < 0 || weaponSlot > _weapons.Length - 1) return;

        if(_weapons == null) _weapons = new Weapon[_weaponSlotAmount];

        if (_weapons[weaponSlot] == null) _weapons[weaponSlot] = ScriptableObject.CreateInstance<Weapon>();

        _weapons[weaponSlot].WeaponPower = newWeapon.WeaponPower;
        _weapons[weaponSlot].ArmorPenetraion = newWeapon.ArmorPenetraion;
        _weapons[weaponSlot].BulletSpeed = newWeapon.BulletSpeed;
        _weapons[weaponSlot].HeatmeterUsage = newWeapon.HeatmeterUsage;
        _weapons[weaponSlot].BulletPrefab = newWeapon.BulletPrefab;
        _weapons[weaponSlot].FireRate = newWeapon.FireRate;
    }
    
    private void ReduceWeaponsCooldown()
    {
        if (_weapons == null) return;

        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i].IsOnCooldown) _weapons[i].CurrentCooldown -= Time.deltaTime;
        }
    }

    public void FireWeapons()
    {
        if (_playerController == null) return;
        if (_heatmeter == null) return;

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].Shoot(_heatmeter, _playerController.PlayerStats, _firePoints[i]);
        }
    }

    public void Fire() => FireWeapons();
}
