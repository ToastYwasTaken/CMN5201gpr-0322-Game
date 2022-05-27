using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

public class WeaponComp : MonoBehaviour, IWeapon
{
    [SerializeField] private Weapon _weaponItem;
    [SerializeField] private GameObject _muzzle;
    [SerializeField] private Enemy _enemyStats;

    [SerializeField] private Transform _parent;

    [SerializeField] private bool _useRandomWeapons;
    [SerializeField] private Weapon[] _randomWeapons;

    [SerializeField] private AudioManager _audioManager;

    private WeaponSlot _weaponSlot;

    private void Awake()
    {
       if(_enemyStats == null) _enemyStats = GetComponentInChildren<Enemy>();
        if (_audioManager == null) FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        if (_weaponItem == null) 
        {
            Debug.LogWarning("Enemy has no weapon equiped!");
            return;
        }
        else _weaponSlot = new WeaponSlot(PickWeapon());
    }

    private void Update()
    {
       ReduceWeaponCooldown();
    }



    public void Fire()
    {
        if (_weaponSlot.WeaponItem ==null) return;
        if (_muzzle == null)
        {
            Debug.LogWarning("Enemy has no muzzle!");
            return;
        } 

        _weaponSlot.Shoot(null, false, _enemyStats, _muzzle, _parent, _audioManager);
    }

    public void ChangeWeapon(Weapon newWeapon)
    {
        if (newWeapon == null) return;
        _weaponSlot = new WeaponSlot(newWeapon);
    }

    private void ReduceWeaponCooldown()
    {
        if (_weaponSlot.WeaponItem == null) return;
        if (_weaponSlot.IsOnCooldown) _weaponSlot.CurrentCooldown -= Time.deltaTime;
    }

    private Weapon PickWeapon()
    {
        if (_useRandomWeapons)
        {
            return _randomWeapons[Random.Range(0, _randomWeapons.Length)];
        }
        else return _weaponItem;
    }
}

//[SerializeField] private GameObject _projectilePfab;
//[SerializeField] private Transform[] _muzzles;
//[SerializeField] private float _fireRate, _coolDownTime;
//[SerializeField] private int _shotNum;
//private bool _canShoot = true;
//private int _currentShot = 0;

//private void Awake()
//{
//    if (_muzzles.Length < 1) _canShoot = false;
//}
//public void Fire()
//{
//    if (!_canShoot) return;

//    GameObject projectile = Instantiate(_projectilePfab, NextMuzzle());
//    projectile.transform.parent = null;

//    if (_coolDownTime > 0)
//    {
//        _currentShot++;
//        if (_currentShot >= _shotNum)
//        {
//            _currentShot = 0;
//            _canShoot = false;
//            StartCoroutine(Timer(_coolDownTime));
//            return;
//        }
//    }
//    _canShoot = false;
//    StartCoroutine(Timer(_fireRate));
//}

//private IEnumerator Timer(float time)
//{
//    yield return new WaitForSeconds(time);
//    _canShoot = true;
//}

//int currMuzzle = 0;
//Transform NextMuzzle()
//{
//    if (currMuzzle >= _muzzles.Length)
//        currMuzzle = 0;
//    currMuzzle++;
//    return _muzzles[currMuzzle - 1];
//}

