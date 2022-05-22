using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Heatlevel_Percentage", order = 100)]
public class HeatlevelPercentageTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.Heatmeter.HeatmeterPercentage, checkValue, checkOperator);
    }
}
