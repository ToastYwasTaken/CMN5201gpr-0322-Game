using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Armor_Broken", order = 100)]
public class ArmorBrokenTrigger : OverdriveTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return playerInformation.PlayerStats.IsArmorBroken;
    }
}