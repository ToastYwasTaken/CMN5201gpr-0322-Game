/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ShotBehaviour.cs
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
using FirstGearGames.SmoothCameraShaker;
using UnityEngine;

public abstract class ShotBehaviour : ScriptableObject
{
    public abstract void Fire(ProjectileStats projectileStats,
                              GameObject bulletPrefab, GameObject firePoint, Transform parent, 
                              WeaponAudio weaponManager,
                              WeaponScreenshake weaponScreenshake);

    protected virtual void PlaySound(WeaponAudio weaponAudio)
    {
        if(weaponAudio.AudioManager == null)
        {
            Debug.LogWarning("No Audiomanager found!");
            return;
        }

        if (weaponAudio.WeaponShootSound == null)
        {
            Debug.LogWarning("Weapon shoot sound not set!");
            return;
        }

        weaponAudio.AudioManager.PlaySound(weaponAudio.WeaponShootSound);
    }

    protected virtual void Screenshake(WeaponScreenshake weaponScreenshake)
    {
        if (!weaponScreenshake.UseScreenshake) return;
        if (weaponScreenshake.ShakeData == null) return;

        CameraShakerHandler.Shake(weaponScreenshake.ShakeData);
    }
}
