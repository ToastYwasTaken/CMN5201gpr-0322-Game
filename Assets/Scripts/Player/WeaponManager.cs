using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class WeaponManager : MonoBehaviour, IShoot
{
    [SerializeField] private PlayerCore _playerCore;
    [SerializeField] private Heatmeter _heatmeter;

    private PlayerInformation _playerInformation;

    private Weapon[] _weapons;
    [SerializeField] private Weapon[] _weaponItems;
    [SerializeField] private Weapon _defaultWeapon;

    private Weapon[] _tempWeaponItems;

    [SerializeField] private GameObject[] _firePoints;

    private readonly int _weaponSlotAmount = 2;

    #region Unity Calls
    private void Awake()
    {
        if (_weaponItems == null) _weaponItems = new Weapon[_weaponSlotAmount];

        if (_playerCore == null)
            _playerCore = GetComponent<PlayerCore>();


    }

    private void Start()
    {
        _playerInformation = _playerCore.PlayerInformation;
        _heatmeter = _playerInformation.Heatmeter;


        InitializeWeapons();

        LoadWeapons();
    }


    private void Update()
    {
        ReduceWeaponsCooldown();

        CheckIfWeaponsChanged();
    }
    #endregion

    #region Startup Methods
    private void InitializeWeapons()
    {
        _weapons = new Weapon[_weaponSlotAmount];

        for (int i = 0; i < _weapons.Length; i++)
        {
            SetupWeapon(_defaultWeapon, i);
        }

        _tempWeaponItems = new Weapon[_weaponSlotAmount];

        for (int i = 0; i < _tempWeaponItems.Length; i++)
        {
            _tempWeaponItems[i] = Instantiate(_defaultWeapon);
        }
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
    #endregion

    #region Recurring Methods
    private void CheckIfWeaponsChanged()
    {
        if (_weaponItems == null) _weaponItems = new Weapon[_weaponSlotAmount];

        for (int i = 0; i < _weaponItems.Length; i++)
        {
            if (_weaponItems[i] == null) return;
            if (_tempWeaponItems[i] == null) return;

            if (_weaponItems[i].ID != _tempWeaponItems[i].ID)
            {
                SetupWeapon(_weaponItems[i], i);
                _tempWeaponItems[i] = Instantiate(_weaponItems[i]);
            }
        }
    }

    private void ReduceWeaponsCooldown()
    {
        if (_weapons == null) return;

        for (int i = 0; i < _weapons.Length; i++)
        {
            if (_weapons[i].IsOnCooldown) _weapons[i].CurrentCooldown -= Time.deltaTime;
        }
    }
    #endregion

    #region Called Methods
    private void SetupWeapon(Weapon newWeapon, int weaponSlot)
    {
        if (newWeapon == null) return;
        if (weaponSlot < 0 || weaponSlot > _weapons.Length - 1) return;

        if (_weapons == null) _weapons = new Weapon[_weaponSlotAmount];

        if (_weapons[weaponSlot] == null) _weapons[weaponSlot] = Instantiate(_defaultWeapon);

        _weapons[weaponSlot] = Instantiate(newWeapon);
    }

    public void Shoot()
    {
        if (_playerCore == null) return;
        if (_heatmeter == null) return;

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].Shoot(_heatmeter, _playerInformation.PlayerStats, _firePoints[i]);
        }
    }
    #endregion
}
