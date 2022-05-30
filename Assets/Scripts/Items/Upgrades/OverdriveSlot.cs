using Assets.Scripts.Player;

public class OverdriveSlot
{
    private float _currentCooldown;
    public float CurrentCooldown
    {
        get => _currentCooldown;
        set
        {
            if (value < 0) value = 0;
            if (value > _overdriveItem.Cooldown) value = _overdriveItem.Cooldown;

            if (value > 0) _isOnCooldown = true;
            if (value == 0) _isOnCooldown = false;

            _currentCooldown = value;
        }
    }

    protected bool _isOnCooldown;
    public bool IsOnCooldown { get => _isOnCooldown; }

    private bool _effectsActivated = false;

    private OverdriveChip _overdriveItem;
    public OverdriveSlot(OverdriveChip overdriveItem) => OverdriveItem = overdriveItem;

    public OverdriveChip OverdriveItem
    {
        get => _overdriveItem;
        set => _overdriveItem = value;
    }

    public void UseOverdrive(PlayerInformation playerInformation)
    {
        OverdriveBehaviours(playerInformation);
    }

    public void OverdriveEffects(PlayerInformation playerInformation)
    {
        if (_overdriveItem == null) return;
        if (!_effectsActivated && _overdriveItem.ActivateEffects(playerInformation))
        {
            _effectsActivated = true;
        }
        else if (_effectsActivated) DeactivateEffects(playerInformation);
    }

    public void DeactivateEffects(PlayerInformation playerInformation)
    {
        if (_overdriveItem == null) return;
        if (!_effectsActivated) return;
        _overdriveItem.DeactivateEffects(playerInformation);
    }

    public void Unequip(PlayerInformation playerInformation)
    {
        if(_overdriveItem == null) return;
        DeactivateEffects(playerInformation);
        _overdriveItem.OnUnequip(playerInformation);
    }

    public void Equip(PlayerInformation playerInformation)
    {
        if (_overdriveItem == null) return;
        _overdriveItem.OnEquip(playerInformation);
    }

    private void OverdriveBehaviours(PlayerInformation playerInformation)
    {
        if (_isOnCooldown) return;
        if (_overdriveItem == null) return; 

        if (_overdriveItem.ActivateBehaviours(playerInformation))
        {
            _isOnCooldown = true;
            _currentCooldown = _overdriveItem.Cooldown;
        }
    }
}