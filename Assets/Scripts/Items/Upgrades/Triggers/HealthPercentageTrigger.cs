using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Health_Percentage", order = 100)]
public class HealthPercentageTrigger : OverdriveTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.HealthPercentage, checkValue, checkOperator);
    }
}
