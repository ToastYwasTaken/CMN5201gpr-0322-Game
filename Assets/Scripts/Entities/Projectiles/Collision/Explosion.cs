using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public ProjectileStats ProjectileStats;
    [HideInInspector] public float ExplosionRadius;

    private void Start()
    {
        ExplosionEffect(ProjectileStats, ExplosionRadius);
        PlayExplosionSound(ProjectileStats.WeaponAudio);
    }

    private void ExplosionEffect(ProjectileStats projectileStats, float explosionRadius)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for (int i = 0; i < targets.Length; i++)
        {
            IReturnEntityType hittedObjectIType = targets[i].GetComponent<IReturnEntityType>();

            eEntityType hittedType;
            if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
            else continue;

            if (hittedType == projectileStats.ProjectileOwnerType) continue;

            IDamageable damageable = targets[i].GetComponent<IDamageable>();
            if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration,
                                                          projectileStats.CanCrit, projectileStats.CritChance);

        }

        Destroy(this.gameObject);
    }

    private void PlayExplosionSound(WeaponAudio weaponAudio)
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
