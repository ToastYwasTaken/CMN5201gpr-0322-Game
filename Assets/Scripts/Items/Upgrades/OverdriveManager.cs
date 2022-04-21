using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class OverdriveManager : MonoBehaviour
{
    [SerializeField] private PlayerCore _playerCore;

    private OverdriveChips[] _overdriveItems;
    private OverdriveChips[] _tempOverdriveItems;

    private readonly int _overdriveChipSlotAmount = 3;

    private void Awake()
    {
        if (_playerCore == null) _playerCore = GetComponent<PlayerCore>();
    }

    private void CheckIfOverdriveChipsChanged()
    {
        if (_overdriveItems == null) _overdriveItems = new OverdriveChips[_overdriveChipSlotAmount];

        for (int i = 0; i < _overdriveItems.Length; i++)
        {
            if (_overdriveItems[i] == null) return;
            if (_tempOverdriveItems[i] == null) return;

            if (_overdriveItems[i].ID != _tempOverdriveItems[i].ID)
            {
                //SetupWeapon(_weaponItems[i], i);
                _tempOverdriveItems[i] = Instantiate(_overdriveItems[i]);
            }
        }
    }
}
