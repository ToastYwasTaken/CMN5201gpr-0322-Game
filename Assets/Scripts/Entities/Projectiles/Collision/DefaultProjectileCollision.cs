/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : DefaultProjectileCollision.cs
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

[CreateAssetMenu(fileName = "New Default Collision", menuName = "Entities/Projectiles/Collision/Default", order = 100)]
public class DefaultProjectileCollision : ProjectileCollision
{
    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        //check if wall
        if (collision.CompareTag("Wall")) AfterCollisionEffects(projectileStats, projectile);

        //check if an enviroment element fired the bullet
        if(projectileStats.ProjectileOwnerType == eEntityType.Environment) CollisionDamage(collision, projectileStats, projectile);

        //check for other entity types
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