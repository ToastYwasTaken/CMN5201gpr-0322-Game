/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : PickupablesEntity.cs
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

public class PickupablesEntity : MonoBehaviour
{
    [SerializeField] private Pickupables _pickupables;

    private void OnTriggerEnter2D(Collider2D collision)
{
        if (_pickupables.PickpupAction == null) return;
        if (_pickupables.PickupCheck == null) return;

        if (_pickupables.PickupCheck.CheckPickup(collision))
            if (_pickupables.PickpupAction.PerformAction(collision)) DestroyPickupables();
    }

    private void DestroyPickupables()
    {
        OnDestroy();
    }

    public virtual void OnDestroy()
    {
        Destroy(gameObject);
    }
}
