using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class WeaponManager : MonoBehaviour, IShoot, IEquipWeapons
{
    [SerializeField] private PlayerCore _playerCore;
    [SerializeField] private Heatmeter _heatmeter;

    private PlayerInformation _playerInformation;

    private WeaponSlot[] _weaponsSlots;
    public WeaponSlot[] WeaponsSlots { get => _weaponsSlots; }
    [SerializeField] private Weapon _defaultWeapon;

    [SerializeField] private GameObject[] _firePoints;
    [SerializeField] private Transform _parent;

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
            if (_weaponsSlots[i].WeaponItem == null) return;
            if (_weaponsSlots[i].IsOnCooldown) _weaponsSlots[i].CurrentCooldown -= Time.deltaTime;
        }
    }
    #endregion

    #region Interface Methods
    public void Shoot()
    {
        if (_playerCore == null) return;
        if (_heatmeter == null) return;

        for (int i = 0; i < _weaponsSlots.Length; i++)
        {
            if (_weaponsSlots[i].WeaponItem == null) return;
            _weaponsSlots[i].Shoot(_heatmeter, true, _playerInformation.PlayerStats, _firePoints[i], _parent);
        }
    }

    public bool EquipWeapon(Weapon newWeapon, int weaponSlot)
    {
        if (newWeapon == null) return false;
        if (_weaponsSlots == null) InitializeWeapons();
        if (weaponSlot < 0 && weaponSlot > (_weaponsSlots.Length -1)) return false;

        _weaponsSlots[weaponSlot] = new WeaponSlot(newWeapon);
        return true;
    }
    #endregion

    #region Editor 
    [SerializeField] private Weapon[] _testWeapon = null;

    public void ReloadWeapons()
    {
        if (_testWeapon == null) _testWeapon = new Weapon[_weaponSlotAmount];

        for (int i = 0; i < _weaponsSlots.Length; i++)
        {
            if (_testWeapon[i] == null) return;
            _weaponsSlots[i].WeaponItem = _testWeapon[i];
        }
    }    
    #endregion
}
