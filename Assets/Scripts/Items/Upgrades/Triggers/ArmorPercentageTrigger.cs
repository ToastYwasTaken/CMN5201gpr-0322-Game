using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Armor_Percentage", order = 100)]
public class ArmorPercentageTrigger : OverdriveTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.ArmorPercentage, checkValue, checkOperator);
    }
}
