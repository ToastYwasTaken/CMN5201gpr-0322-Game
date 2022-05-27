using UnityEngine;

public struct ProjectileStats
{
    public float AttackPower;
    public float ArmorPenetration;
    public float ProjectileSpeed;
    public bool CanCrit;
    public float CritChance;

    public eEntityType ProjectileOwnerType;
    public GameObject ProjectileSender;

    public WeaponAudio WeaponAudio;

    public float ProjectileLifeTime;
}

public enum eEntityType 
{
    Player, 
    Enemy, 
    Environment
}
