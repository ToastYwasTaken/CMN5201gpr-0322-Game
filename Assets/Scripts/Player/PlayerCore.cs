using UnityEngine;

public class PlayerCore : MonoBehaviour
{

    private PlayerInformation _playerInformation;
    public PlayerInformation PlayerInformation { get => _playerInformation; }

    [SerializeField] private EntityStats _playerStats;
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private Heatmeter _heatmeter;
    [SerializeField] private OverdriveManager _overdriveManager;
    [SerializeField] private AudioManager _audioManager;

    private void Awake()
    {
        _playerInformation = new PlayerInformation();

        if (_playerStats == null) _playerStats = GetComponent<EntityStats>();
        if (_heatmeter == null) _heatmeter = GetComponent<Heatmeter>();
        if (_weaponManager == null) _weaponManager = GetComponent<WeaponManager>();
        if (_overdriveManager == null) _overdriveManager = GetComponent<OverdriveManager>();

        if(_audioManager == null) _audioManager = FindObjectOfType<AudioManager>();


        _playerInformation.Heatmeter = _heatmeter;
        _playerInformation.WeaponManager = _weaponManager;
        _playerInformation.PlayerStats = _playerStats;
        _playerInformation.OverdriveManager = _overdriveManager;
        _playerInformation.AudioManager = _audioManager;
    }
}
