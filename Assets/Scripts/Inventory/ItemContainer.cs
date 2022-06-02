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

using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D))]
public class ItemContainer : MonoBehaviour
{
    [SerializeField] Item _item;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] eItemType _itemType;
    public Item Item { set { _item = value; }}
    public eItemType ItemType { set { _itemType = value; }}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_item == null) return;
        PlayerController2 pContrl = collision.gameObject.GetComponent<PlayerController2>();
        if (pContrl != null)
            if(RefLib.sInventory.PickupItem(_item, _spriteRenderer.sprite, _spriteRenderer.color, _itemType))
                Destroy(gameObject);
        //if usable > pContrl.stats.Changestats()
    }
}

public enum eItemType
{
    ALL,
    WEAPON,
    CHIP
}
