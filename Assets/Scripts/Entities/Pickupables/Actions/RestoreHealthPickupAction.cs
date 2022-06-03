/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : RestoreHealthPickupAction.cs
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

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Restore Health", order = 100)]
public class RestoreHealthPickupAction : PickpupAction
{
    [SerializeField] private float _restoreHealthAmount;

    public override bool PerformAction(Collider2D collision) 
    {
       IRestoreHealth restoreHealth = collision.GetComponent<IRestoreHealth>();
        if (restoreHealth != null)
        {
            restoreHealth.RestoreHealth(_restoreHealthAmount);
            return true;
        }
        else return false;
    }
}
