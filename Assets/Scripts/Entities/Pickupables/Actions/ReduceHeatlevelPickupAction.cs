/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ReduceHeatlevelPickupAction.cs
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

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Reduce Heatlevel", order = 100)]
public class ReduceHeatlevelPickupAction : PickpupAction
{
    [SerializeField] private float _reduceHeatlevelAmount;

    public override bool PerformAction(Collider2D collision)
    {
        IReduceHeatlevel reduceHeatlevel = collision.GetComponent<IReduceHeatlevel>();
        if (reduceHeatlevel != null)
        {
            reduceHeatlevel.ReduceHeatlevel(_reduceHeatlevelAmount);
            return true;
        }
        else return false;
    }
}
