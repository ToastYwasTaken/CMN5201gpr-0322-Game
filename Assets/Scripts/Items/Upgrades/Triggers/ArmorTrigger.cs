using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Player;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Armor", order = 100)]
public class ArmorTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.Armor, checkValue, checkOperator);
    }
}
