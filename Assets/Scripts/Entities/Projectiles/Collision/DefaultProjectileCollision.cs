using UnityEngine;

[CreateAssetMenu(fileName = "New Default Collision", menuName = "Entities/Projectiles/Collision/Default", order = 100)]
public class DefaultProjectileCollision : ProjectileCollision
{
    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        if (collision.CompareTag("Wall")) AfterCollisionEffects(projectileStats, projectile);

        if(projectileStats.ProjectileOwnerType == eEntityType.Environment) CollisionDamage(collision, projectileStats, projectile);

        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return;

        if (hittedType == projectileStats.ProjectileOwnerType) return;
        CollisionDamage(collision, projectileStats, projectile);
    }

    private void CollisionDamage(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration,
                                                      projectileStats.CanCrit, projectileStats.CritChance);

        AfterCollisionEffects(projectileStats, projectile);
    }

    private void AfterCollisionEffects(ProjectileStats projectileStats, GameObject projectile)
    {
        PlayCollisionSound(projectileStats.WeaponAudio);
        Destroy(projectile.gameObject);
    }
}