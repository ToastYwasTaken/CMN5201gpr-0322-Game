using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Effect", menuName = "Items/Overdrive_Chips/Equip Effect/Increase Crit", order = 100)]
public class OverdriveIncreaseCritChanceEquipEffect : OverdriveEquipEffect
{
    [SerializeField] private float _increaseCritChance;

    public override void ActivateEffects(PlayerInformation playerInformation) 
    {
        playerInformation.PlayerStats.CritChanceModifier += _increaseCritChance;
    }

    public override void DeactivateEffects(PlayerInformation playerInformation) 
    {
        playerInformation.PlayerStats.CritChanceModifier -= _increaseCritChance;
    }
}