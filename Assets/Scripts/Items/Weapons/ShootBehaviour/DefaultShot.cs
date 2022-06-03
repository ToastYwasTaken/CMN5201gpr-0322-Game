/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : DefaultShot.cs
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

[CreateAssetMenu(fileName = "New Shot Behaviour", menuName = "Items/Weapons/Shot Behaviour/Default", order = 100)]
public class DefaultShot : ShotBehaviour
{
    public override void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint, Transform parent, 
                              WeaponAudio weaponAudio, WeaponScreenshake weaponScreenshake)
    {
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
        PlaySound(weaponAudio);
        Screenshake(weaponScreenshake);

        newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;
        newBullet.transform.SetParent(parent);
    }
}