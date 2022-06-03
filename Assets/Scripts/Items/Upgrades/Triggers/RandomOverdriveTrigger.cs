/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : RandomOverdriveTrigger.cs
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

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Random", order = 100)]
public class RandomOverdriveTrigger : OverdriveTrigger
{
    [SerializeField] private float _cooldownTime;
    private float _currentCooldownTime;

    [SerializeField, Range(0, 100)] private float _triggerChance;
    

    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        bool isValid;

        if(_currentCooldownTime < 0)
        {
            if ((_triggerChance / 100f) > Random.Range(0f, 1f)) isValid = true;
            else isValid = false;
            _currentCooldownTime = _cooldownTime;
            
        } else 
        {
            _currentCooldownTime -= Time.deltaTime;
            isValid = false;
        }

        if (intervetd) return !isValid;
        else return isValid;
    }
}
