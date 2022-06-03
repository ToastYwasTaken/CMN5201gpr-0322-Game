/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveManager.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
using UnityEngine;

[RequireComponent(typeof(PlayerCore))]
public class OverdriveManager : MonoBehaviour, IEquipOverdriveChip
{
    [SerializeField] private PlayerCore _playerCore;

    private PlayerInformation _playerInformation;

    private OverdriveSlot[] _overdriveSlots;
    public OverdriveSlot[] OverdriveSlots { get => _overdriveSlots; }

    [SerializeField] private OverdriveChip _defaultOverdriveChip;

    private readonly int _overdriveSlotAmount = 1;

    #region Unity Calls
    private void Awake()
    {
        if (_playerCore == null)
            _playerCore = GetComponent<PlayerCore>();

        InitializeOverdrive();
    }

    private void Start()
    {
        _playerInformation = _playerCore.PlayerInformation;
    }

    private void Update()
    {
        ReduceOverdriveCooldown();

        CheckAndUseOverdrive();
    }

    private void CheckAndUseOverdrive()
    {
        for (int i = 0; i < _overdriveSlots.Length; i++)
        {
            if (_overdriveSlots[i].OverdriveItem == null) return;
            _overdriveSlots[i].UseOverdrive(_playerInformation);
        }
    }
    #endregion

    #region Startup Methods
    private void InitializeOverdrive()
    {
        if (_overdriveSlots != null) return;
        _overdriveSlots = new OverdriveSlot[_overdriveSlotAmount];

        for (int i = 0; i < _overdriveSlots.Length; i++)
        {
            _overdriveSlots[i] = new OverdriveSlot(_defaultOverdriveChip);
        }
    }
    #endregion

    #region Recurring Methods
    private void ReduceOverdriveCooldown()
    {
        if (_overdriveSlots == null) InitializeOverdrive();

        for (int i = 0; i < _overdriveSlots.Length; i++)
        {
            if (_overdriveSlots[i].OverdriveItem == null) return;
            if (_overdriveSlots[i].IsOnCooldown) _overdriveSlots[i].CurrentCooldown -= Time.deltaTime;
        }
    }
    #endregion

    #region Interfaces

    public bool EquipOverdriveChip(OverdriveChip newOverdriveChip, int overdriveSlot)
    {
        if (newOverdriveChip == null) return false;
        if (_overdriveSlots == null) InitializeOverdrive();
        if (overdriveSlot < 0 && overdriveSlot > (_overdriveSlots.Length -1)) return false;

        _overdriveSlots[overdriveSlot].Unequip(_playerInformation);

        _overdriveSlots[overdriveSlot] = new OverdriveSlot(newOverdriveChip);
        _overdriveSlots[overdriveSlot].Equip(_playerInformation);
        return true;
    }

    public bool UnequipOverdriveChip(int overdriveSlot) 
    {
        if (_overdriveSlots == null) InitializeOverdrive();
        if (overdriveSlot < 0 && overdriveSlot > (_overdriveSlots.Length -1)) return false;

        _overdriveSlots[overdriveSlot].Unequip(_playerInformation);
        _overdriveSlots[overdriveSlot].DeactivateEffects(_playerInformation);

        _overdriveSlots[overdriveSlot] = new OverdriveSlot(null);
        return true;
    }
    #endregion


    #region Editor
    [SerializeField] private OverdriveChip[] _testOverdriveChips = null;

    public void ReloadOverdriveChips()
    {
        if (_testOverdriveChips == null) _testOverdriveChips = new OverdriveChip[_overdriveSlotAmount];

        for (int i = 0; i < _overdriveSlots.Length; i++)
        {
            if (i < 0 && i > (_testOverdriveChips.Length -1)) return;
            if (_testOverdriveChips[i] == null) return;
            _overdriveSlots[i].OverdriveItem = _testOverdriveChips[i];
        }
    }
    #endregion
}

