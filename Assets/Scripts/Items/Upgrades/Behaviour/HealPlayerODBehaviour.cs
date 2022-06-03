/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : HealPlayerODBehaviour.cs
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

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Heal_Player", order = 100)]
public class HealPlayerODBehaviour : OverdriveBehaviour
{
    [SerializeField] private float _amountToHeal = 0;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        IRestoreHealth restoreHealth = playerInformation.WeaponManager.gameObject.GetComponent<IRestoreHealth>();
        if (restoreHealth != null)
        {
            restoreHealth.RestoreHealth(_amountToHeal);            
        }
    }
}