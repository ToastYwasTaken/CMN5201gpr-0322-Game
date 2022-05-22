using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Armor_Percentage", order = 100)]
public class ArmorPercentageTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.ArmorPercentage, checkValue, checkOperator);
    }
}
