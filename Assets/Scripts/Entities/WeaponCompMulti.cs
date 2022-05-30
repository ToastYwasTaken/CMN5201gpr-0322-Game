using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

public class WeaponCompMulti : MonoBehaviour, IWeapon
{
    [SerializeField] private Weapon[] _weaponItem;
    [SerializeField] private GameObject[] _muzzles;
    [SerializeField] private Enemy _enemyStats;

    [SerializeField] private Transform _parent;
    [SerializeField] Transform _target;

    [SerializeField] private bool _useRandomWeapons;
    [SerializeField] private Weapon[] _randomWeapons;
    [SerializeField] Rotateable[] _turrets;

    [SerializeField] private AudioManager _audioManager;

    private WeaponSlot[] _weaponSlot;

    public Transform Target { get { return _target; } set { _target = value; } }
    bool hasTurrets = false;

    private void Awake()
    {
        if (_enemyStats == null) _enemyStats = GetComponentInChildren<Enemy>();
        if (_audioManager == null) FindObjectOfType<AudioManager>();
        if (_turrets != null) hasTurrets = true;


    }

    private void Start()
    {
        if (Target == null) Target = RefLib.Player.GetComponent<Transform>();

        _weaponSlot = new WeaponSlot[_weaponItem.Length];

        if (_weaponItem == null)
        {
            Debug.LogWarning("Enemy has no weapon equiped!");
            return;
        }
        else for (int i = 0; i < _weaponSlot.Length; i++)
            {
                _weaponSlot[i] = new WeaponSlot(PickWeapon(i));
            }
    }

    private void Update()
    {
        ReduceWeaponCooldown();
    }
    private void FixedUpdate()
    {
        if (hasTurrets)
            DoTurrets();
    }

    int _currentMuzzle = -1;
    GameObject GetMuzzle()
    {
        if (_muzzles == null) return gameObject;
        _currentMuzzle = (_currentMuzzle + 1) % _muzzles.Length;
        return _muzzles[_currentMuzzle];
    }
    void DoTurrets()
    {
        foreach (Rotateable turret in _turrets)
        {
            turret.RotateTowardsTargetT(Target);
        }
    }

    public void Fire()
    {
        foreach (WeaponSlot wSlot in _weaponSlot)
        {
            if (wSlot.WeaponItem ==null) return;
            if (_muzzles == null)
            {
                Debug.LogWarning("Enemy has no muzzle!");
                return;
            }

            wSlot.Shoot(null, false, _enemyStats, GetMuzzle(), _parent, _audioManager);
        }
    }

    public void ChangeWeapon(Weapon newWeapon, int slot = 0)
    {
        if (newWeapon == null) return;
        _weaponSlot[slot] = new WeaponSlot(newWeapon);
    }

    private void ReduceWeaponCooldown()
    {
        foreach (WeaponSlot wSlot in _weaponSlot)
        {
            if (wSlot.WeaponItem == null) return;
            if (wSlot.IsOnCooldown) wSlot.CurrentCooldown -= Time.deltaTime;
        }
    }

    private Weapon PickWeapon(int index = 0)
    {
        if (_useRandomWeapons)
        {
            return _randomWeapons[Random.Range(0, _randomWeapons.Length)];
        }
        else return _weaponItem[index % (_weaponItem.Length)];
    }
}
