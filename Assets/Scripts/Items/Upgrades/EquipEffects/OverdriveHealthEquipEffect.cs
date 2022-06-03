/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : OverdriveHeatingEquipEffect.cs
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

[CreateAssetMenu(fileName = "New Equip Effect", menuName = "Items/Overdrive_Chips/Equip Effect/Increase Health", order = 100)]
public class OverdriveHealthEquipEffect : OverdriveEquipEffect
{
    [SerializeField] private float _increaseHealth;

    public override void ActivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.MaxHealthModifier += _increaseHealth;
    }

    public override void DeactivateEffects(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.MaxHealthModifier -= _increaseHealth;
    }
}