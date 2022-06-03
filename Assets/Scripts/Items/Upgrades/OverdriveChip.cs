/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveChip.cs
* Date   : 03.06.2022
* Author : Alexander Sigmund (AS)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*
******************************************************************************/
using UnityEngine;

[CreateAssetMenu(fileName = "New Chip", menuName = "Items/Overdrive_Chips/Chip", order = 100)]
public class OverdriveChip : Item
{
    [SerializeField] private float _cooldown;
    public float Cooldown { get => _cooldown; }

    [SerializeField] OverdriveBehaviour[] _overdriveBehaviours;
    [SerializeField] OverdriveTrigger[] _overdriveTriggers;
    [SerializeField] OverdriveEffect[] _overdriveEffects;
    [SerializeField] OverdriveEquipEffect[] _overdriveEquipEffects;

    public void OnEquip(PlayerInformation playerInformation)
    {
        if (_overdriveBehaviours == null) Debug.LogWarning("Overdrive Behaviour wasnt set!");
        if (_overdriveTriggers == null) Debug.LogWarning("Overdrive Trigger wasnt set!");

        if (_overdriveTriggers == null || _overdriveTriggers.Length == 0)
        {
            for (int i = 0; i < _overdriveTriggers.Length; i++)
            { 
                if (_overdriveTriggers == null) continue;
                _overdriveTriggers[i].SetupEventTriggers();
            }
        }

        
        for (int i = 0; i < _overdriveEquipEffects.Length; i++)
        {
            if (_overdriveTriggers == null) continue;
            _overdriveEquipEffects[i].ActivateEffects(playerInformation);
        }
    }

    public void OnUnequip(PlayerInformation playerInformation)
    {
        if (_overdriveTriggers == null || _overdriveTriggers.Length == 0)
        {
            for (int i = 0; i < _overdriveTriggers.Length; i++)
            {
                if (_overdriveTriggers == null) continue;
                _overdriveTriggers[i].RemoveEventTriggers();
            }
        }        

        for (int i = 0; i < _overdriveEquipEffects.Length; i++)
        {
            if (_overdriveTriggers == null) continue;
            _overdriveEquipEffects[i].DeactivateEffects(playerInformation);
        }
    }

    public bool ActivateBehaviours(PlayerInformation playerInformation)
    {
        if (_overdriveBehaviours == null || _overdriveBehaviours.Length == 0) return false;

        if (CheckTriggers(playerInformation))
        {
            for (int i = 0; i < _overdriveBehaviours.Length; i++)
            {
                if (_overdriveBehaviours[i] == null) continue;
                _overdriveBehaviours[i].UseOverdriveEffect(playerInformation);
            }
            return true;
        }
        else return false;
    }

    public bool ActivateEffects(PlayerInformation playerInformation)
    {
        if (_overdriveEffects == null || _overdriveEffects.Length == 0) return false;

        if (CheckTriggers(playerInformation))
        {
            for (int i = 0; i < _overdriveEffects.Length; i++)
            {
                if (_overdriveEffects[i] == null) continue;
                _overdriveEffects[i].ActivateEffects(playerInformation);
            }
            return true;
        }
        else return false;
    }

    public void DeactivateEffects(PlayerInformation playerInformation)
    {
        if (_overdriveEffects == null || _overdriveEffects.Length == 0) return;

        for (int i = 0; i < _overdriveEffects.Length; i++)
        {
            if (_overdriveEffects[i] == null) continue;
            _overdriveEffects[i].DeactivateEffects(playerInformation);
        }
    }

    private bool CheckTriggers(PlayerInformation playerInformation)
    {
        bool isValid = true;

        if (_overdriveTriggers == null || _overdriveTriggers.Length == 0) return false;

        for (int i = 0; i < _overdriveTriggers.Length; i++)
        {
            if (_overdriveTriggers == null)
            {
                isValid = false;
                continue;
            }
            else if(!_overdriveTriggers[i].CheckTriggerCondition(playerInformation)) isValid = false;
        }

        return isValid;
    }
}
