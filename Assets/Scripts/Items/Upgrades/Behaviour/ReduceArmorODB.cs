/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ReduceArmorODB.cs
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

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Reduce Armor", order = 100)]
public class ReduceArmorODB : OverdriveBehaviour
{
    [SerializeField] private float amountToReduce;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        playerInformation.PlayerStats.Armor -= amountToReduce;
    }
}
