using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Chip", menuName = "Items/Overdrive_Chips/Chip", order = 100)]
public class OverdriveChip : Item
{
    [SerializeField] private float _cooldown;
    public float Cooldown { get => _cooldown; }

    [SerializeField] OverdriveBehaviour[] _overdriveBehaviour;
    [SerializeField] OverdriveTrigger[] _overdriveTrigger;

    public void OnEquip()
    {
        if (_overdriveBehaviour == null) Debug.LogWarning("Overdrive Behaviour wasnt set!");
        if (_overdriveBehaviour == null) Debug.LogWarning("Overdrive Trigger wasnt set!");

        for (int i = 0; i < _overdriveTrigger.Length; i++)
        {
            _overdriveTrigger[i].SetupEventTriggers();
        }
    }

    public void OnUnequip()
    {
        for (int i = 0; i < _overdriveTrigger.Length; i++)
        {
            _overdriveTrigger[i].RemoveEventTriggers();
        }
    }

    public bool ValidateTrigger(PlayerInformation playerInformation)
    {
        if (CheckTriggers(playerInformation))
        {
            for (int i = 0; i < _overdriveBehaviour.Length; i++)
            {
                if (_overdriveBehaviour[i] != null) _overdriveBehaviour[i].UseOverdriveEffect(playerInformation);
            }
            return true;
        }
        else return false;
    }

    private bool CheckTriggers(PlayerInformation playerInformation)
    {
        bool isValid = true;

        for (int i = 0; i < _overdriveTrigger.Length; i++)
        {
            if (!_overdriveTrigger[i].CheckTriggerCondition(playerInformation)) isValid = false;
        }

        return isValid;
    }
}
