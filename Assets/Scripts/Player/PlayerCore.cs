using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCore : MonoBehaviour
{

    private PlayerInformation _playerInformation;
    public PlayerInformation PlayerInformation { get => _playerInformation; }

    [SerializeField] private EntityStats _playerStats;
    [SerializeField] private WeaponManager _weaponManager;
    [SerializeField] private Heatmeter _heatmeter;

    private void Awake()
    {
        _playerInformation = new PlayerInformation();

        if (_playerStats == null) _playerStats = GetComponent<EntityStats>();
        if (_heatmeter == null) _heatmeter = GetComponent<Heatmeter>();
        if (_weaponManager == null) _weaponManager = GetComponent<WeaponManager>();

        _playerInformation.Heatmeter = _heatmeter;
        _playerInformation.WeaponManager = _weaponManager;
        _playerInformation.PlayerStats = _playerStats;
    }
}
