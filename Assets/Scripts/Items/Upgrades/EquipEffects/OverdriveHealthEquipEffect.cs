using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Effect", menuName = "Items/Overdrive_Chips/Equip Effect/Increase Health", order = 100)]
public class OverdriveHealthEquipEffect : OverdriveEquipEffect
{
    [SerializeField] private float _increaseHealth;

    public override void ActivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.MaxHealthModifier += _increaseHealth;
    }

    public override void DeactivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.MaxHealthModifier -= _increaseHealth;
    }
}