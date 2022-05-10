using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Heatlevel_Percentage", order = 100)]
public class HeatlevelPercentageTrigger : OverdriveTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.Heatmeter.HeatmeterPercentage, checkValue, checkOperator);
    }
}
