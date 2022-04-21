using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverdriveChips : Item
{
    [SerializeField] OverdriveBehaviour _overdriveBehaviour;
    [SerializeField] OverdriveTrigger _overdriveTrigger;

    public void OnEquip()
    {
        if (_overdriveBehaviour == null) Debug.LogWarning("Overdrive Behaviour wasnt set!");
        if (_overdriveBehaviour == null) Debug.LogWarning("Overdrive Trigger wasnt set!");

        _overdriveTrigger.SetupEventTriggers();
    }

    public void OnUnequip()
    {
        _overdriveTrigger.RemoveEventTriggers();
    }

    public void ValidateTrigger(PlayerInformation playerInformation)
    {
        if (_overdriveTrigger.CheckTriggerCondition(playerInformation)) _overdriveBehaviour.UseOverdriveEffect(playerInformation);
    }    
}
