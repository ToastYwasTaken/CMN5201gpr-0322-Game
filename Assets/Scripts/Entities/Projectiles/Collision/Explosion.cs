using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public ProjectileStats ProjectileStats;
    [HideInInspector] public float ExplosionRadius;

    [Header("Particle Effect")]
    [SerializeField] private bool _useParticales;
    [SerializeField ]private ParticleSystem _particleSystem;


    private void Start()
    {
        ExplosionEffect(ProjectileStats, ExplosionRadius);
        PlayExplosionSound(ProjectileStats.WeaponAudio);
        PlayParticleEffects();
    }

    private void ExplosionEffect(ProjectileStats projectileStats, float explosionRadius)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for (int i = 0; i < targets.Length; i++)
        {
            if (projectileStats.ProjectileOwnerType == eEntityType.Environment) DealDamage(projectileStats, targets, i);
            else
            {
                IReturnEntityType hittedObjectIType = targets[i].GetComponent<IReturnEntityType>();

                eEntityType hittedType;
                if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
                else continue;

                if (hittedType == projectileStats.ProjectileOwnerType) continue;

                DealDamage(projectileStats, targets, i);
            }
        }

        if (!_useParticales) Destroy(this.gameObject);
    }

    private void DealDamage(ProjectileStats projectileStats, Collider2D[] targets, int i)
    {
        IDamageable damageable = targets[i].GetComponent<IDamageable>();
        if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration,
                                                      projectileStats.CanCrit, projectileStats.CritChance);
    }


    private void PlayExplosionSound(WeaponAudio weaponAudio)
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

    private void PlayParticleEffects()
    {
        if (!_useParticales) return;
        if (_particleSystem == null) return;
        else _particleSystem.Play();
    }

    private void OnParticleSystemStopped()
    {
        Destroy(this.gameObject);
    }
}
