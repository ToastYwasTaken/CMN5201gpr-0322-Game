using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Health_Percentage", order = 100)]
public class HealthPercentageTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.HealthPercentage, checkValue, checkOperator);
    }
}
