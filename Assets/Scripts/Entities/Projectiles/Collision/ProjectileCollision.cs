using System;
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

        if (weaponAudio.WeaponInpactSound == null)
        {
            Debug.LogWarning("Weapon impact sound not set!");
            return;
        }

        weaponAudio.AudioManager.PlaySound(weaponAudio.WeaponInpactSound);
    }
}
