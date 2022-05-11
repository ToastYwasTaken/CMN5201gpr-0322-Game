using UnityEngine;

[CreateAssetMenu(fileName = "New Default Collision", menuName = "Entities/Projectiles/Collision/Default", order = 100)]
public class DefaultProjectileCollision : ProjectileCollision
{
    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) damageable.DealDamage(projectileStats.AttackPower, projectileStats.ArmorPenetration);

        Destroy(projectile.gameObject);
    }
}