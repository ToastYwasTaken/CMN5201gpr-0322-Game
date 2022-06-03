/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Loot.cs
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

[System.Serializable]
public class Loot
{
    [SerializeField] private Item _lootDrop;
    public Item LootDrop { get => _lootDrop; }
    [SerializeField] private float _dropChance;
    public float DropChance { get => _dropChance; }
}
