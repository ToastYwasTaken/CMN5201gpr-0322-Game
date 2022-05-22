using UnityEngine;

public abstract class ProjectileCollision : ScriptableObject
{
    public abstract void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile);
}

public class RocketCollision : ProjectileCollision
{
    public override void OnCollision(Collider2D collision, ProjectileStats projectileStats, GameObject projectile)
    {
        IReturnEntityType hittedObjectIType = collision.GetComponent<IReturnEntityType>();

        eEntityType hittedType;
        if (hittedObjectIType != null) hittedType = hittedObjectIType.GetEntityType();
        else return;


    }
}
