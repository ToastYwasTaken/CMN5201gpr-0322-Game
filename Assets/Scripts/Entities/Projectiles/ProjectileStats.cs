using UnityEngine;

public struct ProjectileStats
{
    public float AttackPower;
    public float ArmorPenetration;
    public float ProjectileSpeed;

    public eEntityType ProjectileOwnerType;
    public GameObject ProjectileSender;

    public float ProjectileLifeTime;
}

public enum eEntityType 
{
    Player, 
    Enemy, 
    Environment
}
