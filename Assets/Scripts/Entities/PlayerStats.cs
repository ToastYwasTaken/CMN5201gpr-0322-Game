/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : PlayerStats.cs
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

public class PlayerStats : EntityStats, IRestoreHealth, IRestoreArmor
{
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Item _startWeapon;
    private int _amountOfWeaponSpawns = 2;

    protected override void Start()
    {
        base.Start();

        if (_itemPrefab == null) return;
        if (_startWeapon == null) return;

        for (int i = 0; i < _amountOfWeaponSpawns; i++)
        {
            CreateItem(_startWeapon, _itemPrefab, gameObject.transform);
        }
    }

    GameObject CreateItem(Item item, GameObject itemPrefab, Transform position)
    {
        GameObject newItem = Instantiate(itemPrefab, position.position, Quaternion.identity);

        newItem.transform.SetParent(null);
        newItem.transform.localScale = Vector3.one/4;

        newItem.GetComponent<ItemContainer>().SetupItem(item);

        return newItem;
    }

    public void RestoreArmor(float amountToRestore)
    {
        if (amountToRestore > 0) Armor += amountToRestore;
    }

    public void RestoreHealth(float amountToRestore)
    {
        if (amountToRestore > 0) Health += amountToRestore;
    }

    protected override void Death()
    {
        base.Death();
    }
}
