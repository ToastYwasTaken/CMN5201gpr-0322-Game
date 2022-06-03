/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ProjectileCollision.cs
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

public abstract class ProjectileCollision : ScriptableObject
{
    public abstract void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile);

    public virtual void PlayCollisionSound(WeaponAudio weaponAudio)
    {
        if (weaponAudio.AudioManager == null)
        {
            Debug.LogWarning("No Audiomanager found!");
            return;
        }

        if (weaponAudio.WeaponImpactSound == null)
        {
            Debug.LogWarning("Weapon impact sound not set!");
            return;
        }

        weaponAudio.AudioManager.PlaySound(weaponAudio.WeaponImpactSound);
    }
}
