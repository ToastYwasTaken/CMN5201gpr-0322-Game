using UnityEngine;

public class Explosion : MonoBehaviour
{
    [HideInInspector] public ProjectileStats ProjectileStats;
    [HideInInspector] public float ExplosionRadius;

    private void Start()
    {
        ExplosionEffect(ProjectileStats, ExplosionRadius);
    }

    private void ExplosionEffect(ProjectileStats projectileStats, float explosionRadius)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        for (int i = 0; i < targets.Length; i++)
        {
            IReturnEntityType hittedObjectIType = targets[i].GetComponent<IReturnEntityType>();

            eEntityType hittedType;
            if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
            else
            {
                Destroy(this.gameObject);
                return;
            }

            if (hittedType == projectileStats.ProjectileOwnerType)
            {
                Destroy(this.gameObject);
                return;
            }

            IDamageable damageable = targets[i].GetComponent<IDamageable>();
            if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration,
                                                          projectileStats.CanCrit, projectileStats.CritChance);

        }

        Destroy(this.gameObject);
    }
}
