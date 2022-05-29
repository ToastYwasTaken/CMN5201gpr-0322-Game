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
