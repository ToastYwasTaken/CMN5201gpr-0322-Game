/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : LootTable.cs
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

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Entities/Loot Table", order = 100)]
public class LootTable : ScriptableObject
{
    [SerializeField] private Loot[] _lootArray;

    public Item DetermineLoot()
    {

        for (int i = 0; i < _lootArray.Length; i++)
        {
            float randomChance = Random.Range(0f, 1f);

            if (_lootArray[i] == null) continue;

            float dropChance = _lootArray[i].DropChance / 100f;

            if (randomChance <= dropChance) return _lootArray[i].LootDrop;
        }

        return null;
    }
}
