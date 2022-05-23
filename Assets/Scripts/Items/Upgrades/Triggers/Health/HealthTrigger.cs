using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Health", order = 100)]
public class HealthTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.Health, checkValue, checkOperator);
    }
}
