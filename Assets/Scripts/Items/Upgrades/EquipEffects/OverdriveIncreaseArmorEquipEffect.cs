using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Effect", menuName = "Items/Overdrive_Chips/Equip Effect/Increase Armor", order = 100)]
public class OverdriveIncreaseArmorEquipEffect : OverdriveEquipEffect
{
    [SerializeField] private float _increaseArmor;

    public override void ActivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.ArmorModifier += _increaseArmor;
    }

    public override void DeactivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.ArmorModifier -= _increaseArmor;
    }
}
