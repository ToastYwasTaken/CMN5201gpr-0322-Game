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

    private WeaponSlot[] _weaponsSlots;  
    [SerializeField] private Weapon _defaultWeapon;

    [SerializeField] private GameObject[] _firePoints;

    private readonly int _weaponSlotAmount = 2;

    #region Unity Calls
    private void Awake()
    {
        if (_playerCore == null)
            _playerCore = GetComponent<PlayerCore>();

        InitializeWeapons();
    }

    private void Start()
    {
        _playerInformation = _playerCore.PlayerInformation;
        _heatmeter = _playerInformation.Heatmeter;
    }

    private void Update()
    {
        ReduceWeaponsCooldown();
    }
    #endregion

    #region Startup Methods
    private void InitializeWeapons()
    {
        if (_weaponsSlots != null) return;
        _weaponsSlots = new WeaponSlot[_weaponSlotAmount];

        for (int i = 0; i < _weaponsSlots.Length; i++)
        {
            _weaponsSlots[i] = new WeaponSlot(_defaultWeapon);
        }
    }
    #endregion

    #region Recurring Methods
    private void ReduceWeaponsCooldown()
    {
        if (_weaponsSlots == null) InitializeWeapons();

        for (int i = 0; i < _weaponsSlots.Length; i++)
        {
            if (_weaponsSlots[i].WeaponInstance == null) return;
            if (_weaponsSlots[i].WeaponInstance.IsOnCooldown) _weaponsSlots[i].WeaponInstance.CurrentCooldown -= Time.deltaTime;
        }
    }
    #endregion

    #region Called Methods
    public void Shoot()
    {
        if (_playerCore == null) return;
        if (_heatmeter == null) return;

        for (int i = 0; i < _weaponsSlots.Length; i++)
        {
            if (_weaponsSlots[i].WeaponInstance == null) return;
            _weaponsSlots[i].WeaponInstance.Shoot(_heatmeter, true, _playerInformation.PlayerStats, _firePoints[i]);
        }
    }

    public void ChangeWeapon(Weapon newWeapon, int weaponSlot)
    {
        if (newWeapon == null) return;
        if (_weaponsSlots == null) InitializeWeapons();
        if (weaponSlot < 0 && weaponSlot > (_weaponsSlots.Length -1)) return;

        _weaponsSlots[weaponSlot] = new WeaponSlot(newWeapon);
    }
    #endregion

    #region Editor 
    [SerializeField] private Weapon _testWeapon1;
    [SerializeField] private Weapon _testWeapon2;

    private void OnValidate()
    {
        if(_testWeapon1 != null)
            ChangeWeapon(_testWeapon1, 0);
        if (_testWeapon2 != null)
            ChangeWeapon(_testWeapon2, 1);
    }
    #endregion
}