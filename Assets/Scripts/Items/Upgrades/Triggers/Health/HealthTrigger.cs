﻿/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : HealthTrigger.cs
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

[CreateAssetMenu(fileName = "New Trigger", menuName = "Items/Overdrive_Chips/Trigger/Float/Health", order = 100)]
public class HealthTrigger : OverdriveFloatTrigger
{
    public override bool CheckTriggerCondition(PlayerInformation playerInformation)
    {
        return CheckTriggers(playerInformation.PlayerStats.Health, checkValue, checkOperator);
    }
}
