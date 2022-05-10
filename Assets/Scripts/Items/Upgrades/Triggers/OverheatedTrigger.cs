using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Overheated", order = 100)]
public class OverheatedTrigger : OverdriveTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return playerInformation.Heatmeter.IsOverheated;
    }
}
