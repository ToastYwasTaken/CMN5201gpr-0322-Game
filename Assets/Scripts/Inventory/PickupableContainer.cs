/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Rotateable.cs
* Date   : 17.04.22
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   25.4.22 JA created 
******************************************************************************/

using UnityEngine;

public class PickupableContainer : MonoBehaviour
{
    private Pickupables _pickupables;
    public Pickupables Pickupables { get => _pickupables; set => _pickupables = value; }

    [SerializeField] private SpriteRenderer _iconSpirteRenderer;

    public void SetupPickupables(Pickupables pickupables)
    {
        _pickupables = pickupables;

        if (_iconSpirteRenderer != null) _iconSpirteRenderer.sprite = _pickupables.ItemSprite;
        if (_iconSpirteRenderer!= null) _iconSpirteRenderer.color = _pickupables.PickupColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_pickupables.PickupCheck.CheckPickup(collision))
            if (_pickupables.PickpupAction.PerformAction(collision)) Destroy(gameObject);
    }
}
