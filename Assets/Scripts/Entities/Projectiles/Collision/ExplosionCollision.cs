/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : ExplosionCollision.cs
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

[CreateAssetMenu(fileName = "New Default Collision", menuName = "Entities/Projectiles/Collision/Explosion", order = 100)]
public class ExplosionCollision : ProjectileCollision
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _explosionObject;

    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        if (collision.CompareTag("Wall")) SpawnExplosion(projectileStats, projectile);

        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return;

        if (hittedType == projectileStats.ProjectileOwnerType) return;

        SpawnExplosion(projectileStats, projectile);        
    }

    private void SpawnExplosion(ProjectileStats projectileStats, GameObject projectile)
    {
        GameObject explosionGO = Instantiate(_explosionObject, projectile.transform);
        if (explosionGO == null) return;
        explosionGO.transform.SetParent(null);

        Explosion explosion = explosionGO.GetComponent<Explosion>();
        if (explosion == null) return;

        explosion.ProjectileStats = projectileStats;
        explosion.ExplosionRadius = _explosionRadius;

        Destroy(projectile);
    }
}