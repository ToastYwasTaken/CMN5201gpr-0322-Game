/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : EntityPickupCheck.cs
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

[CreateAssetMenu(fileName = "New Pickup Check", menuName = "Entities/Pickupables/Pickup Check/Entity Check", order = 100)]
public class EntityPickupCheck : PickupCheck
{
    [SerializeField] private eEntityType _allowedToPickup;

    public override bool CheckPickup(Collider2D collision)
    {
        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return false;

        if (hittedType == _allowedToPickup) return true;
        else return false;
    }
}
