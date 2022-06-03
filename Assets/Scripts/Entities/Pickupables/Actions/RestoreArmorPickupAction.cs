/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : RestoreArmorPickupAction.cs
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

[CreateAssetMenu(fileName = "New Pickup Action", menuName = "Entities/Pickupables/Pickup Action/Restore Armor", order = 100)]
public class RestoreArmorPickupAction : PickpupAction
{
    [SerializeField] private float _restoreArmorAmount;

    public override bool PerformAction(Collider2D collision)
    {
        IRestoreArmor restoreArmor = collision.GetComponent<IRestoreArmor>();
        if (restoreArmor != null)
        {
            restoreArmor.RestoreArmor(_restoreArmorAmount);
            return true;
        }
        else return false;
    }
}
