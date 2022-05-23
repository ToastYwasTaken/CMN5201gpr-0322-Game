using UnityEngine;

[CreateAssetMenu(fileName = "New Default Collision", menuName = "Entities/Projectiles/Collision/Default", order = 100)]
public class DefaultProjectileCollision : ProjectileCollision
{
    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return;

        if (hittedType == projectileStats.ProjectileOwnerType) return;

        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration,
                                                      projectileStats.CanCrit, projectileStats.CritChance);

        Destroy(projectile.gameObject);
    }
}