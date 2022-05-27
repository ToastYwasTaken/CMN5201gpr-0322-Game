using UnityEngine;

public abstract class ShotBehaviour : ScriptableObject
{
    public virtual void Fire(ProjectileStats projectileStats, GameObject bulletPrefab, GameObject firePoint,Transform parent, WeaponAudio weaponManager)
    {

    }

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
}
