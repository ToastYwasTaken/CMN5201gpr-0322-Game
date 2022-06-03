/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Pickupables.cs
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


[CreateAssetMenu(fileName = "New Pickupable", menuName = "Entities/Pickupables/Pickupable", order = 100)]
public class Pickupables : Item
{
    [SerializeField] private PickpupAction _pickpupAction;
    public PickpupAction PickpupAction { get => _pickpupAction; }

    [SerializeField] private PickupCheck _pickupCheck;
    public PickupCheck PickupCheck { get => _pickupCheck; }

    [SerializeField] private Color _pickupColor;
    public Color PickupColor { get => _pickupColor; }
}