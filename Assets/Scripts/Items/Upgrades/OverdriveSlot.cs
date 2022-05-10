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

    private OverdriveChip _overdriveItem;
    public OverdriveSlot(OverdriveChip overdriveItem) => OverdriveItem = overdriveItem;

    public OverdriveChip OverdriveItem
    {
        get => _overdriveItem;
        set
        {
            if (value == null) return;

            if (_overdriveItem != null) _overdriveItem.OnUnequip();

            _overdriveItem = value;

            if (_overdriveItem != null) _overdriveItem.OnEquip();
        }
    }

    public void UseOverdrive(PlayerInformation playerInformation)
    {
        if (_isOnCooldown) return;

        if (_overdriveItem.ValidateTrigger(playerInformation))
        {
            _isOnCooldown = true;
            _currentCooldown = _overdriveItem.Cooldown;
        }
    }
}