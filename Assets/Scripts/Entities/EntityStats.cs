/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : EntityStats.cs
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
using System;
using UnityEngine;

public class EntityStats : MonoBehaviour, IDamageable, IReturnEntityType
{
    public delegate void ChangedHealth(float newHealth);
    public event ChangedHealth OnHealthChanged;
    public event ChangedHealth OnHealthPercentageChanged;

    public delegate void ChangedArmor(float newArmor);
    public event ChangedArmor OnArmorChanged;
    public event ChangedArmor OnArmorPercentageChanged;

    public delegate void EntityDeath();
    public event EntityDeath OnDeath;

    public delegate void ArmorBreak();
    public event ArmorBreak OnArmorBreak;

    public delegate void ArmorRestore();
    public event ArmorRestore OnArmorRestored;

    public delegate void TakeHealthDamage(float damage, bool didCrit);
    public event TakeHealthDamage OnHealthDamageTaken;

    public delegate void TakeArmorDamage(float damage, bool didCrit);
    public event TakeArmorDamage OnArmorDamageTaken;

    [Header("Defensive Stats")]

    //Health
    [SerializeField, Tooltip("The base health without any modifiers")] private float _baseMaxHealth;
    protected float BaseMaxHealth { get => _baseMaxHealth; }
    protected float _maxHealthModifier;

    public float MaxHealthModifier
    {
        get =>  _maxHealthModifier;
        set 
        {
            _maxHealthModifier = value;
            Health = Health;
        }
    }

    public float MaxHealth { get => BaseMaxHealth + MaxHealthModifier; }

    protected float _health;
    public float Health
    {
        get => _health;
        set
        {
            float newHealth = value;

            if (newHealth < 0) newHealth = 0;
            if (newHealth > MaxHealth) newHealth = MaxHealth;

            _health = newHealth;
            if (newHealth == 0)
            {
                Death();
                OnDeath?.Invoke();
            }

            OnHealthChanged?.Invoke(newHealth);
            OnHealthPercentageChanged?.Invoke(HealthPercentageNormalized);
        }
    }

    public float HealthPercentageNormalized { get => Health / MaxHealth *100f; }
    public float HealthPercentage { get => Health / MaxHealth; }

    // Armor
    [SerializeField, Tooltip("The base armor without any modifiers")] private float _baseMaxArmor;
    public float BaseMaxArmor { get => _baseMaxArmor; }

    protected float _armorModifier;
    public float ArmorModifier
    {
        get => _armorModifier;
        set
        {
            _armorModifier = value;
            Armor = Armor;
        }
    }

    public float MaxArmor { get => BaseMaxArmor + ArmorModifier; }

    protected float _armor;
    public float Armor
    {
        get => _armor;
        set
        {
            float newArmor = value;

            if (newArmor < 0) newArmor = 0;
            if (newArmor > MaxArmor) newArmor = MaxArmor;

            if (_isArmorBroken && newArmor > 0) ArmorRestored();

            _armor = newArmor;
            if (newArmor == 0) ArmorBroken();

            OnArmorChanged?.Invoke(newArmor);
            OnArmorPercentageChanged?.Invoke(ArmorPercentage);
        }
    }    

    public float ArmorPercentage { get => (_armor / MaxArmor) * 100f; }

    protected bool _isArmorBroken = false;
    public bool IsArmorBroken { get => _isArmorBroken; }

    // Damage Reduction
    [SerializeField, Tooltip("The base armor penetration reduction without any modifiers")]
    protected float _baseDamageReduction;
    public float BaseDamageReduction { get => _baseDamageReduction; }
    
    private float _damageReductionModifier;
    public float DamageReductionModifier 
    { 
        get => _damageReductionModifier;
        set => _damageReductionModifier = value;
    }

    public float DamageReduction { get => _baseDamageReduction + DamageReductionModifier; }

    // Armor Penetration Reduction
    [SerializeField, Tooltip("The base armor penetration reduction without any modifiers")]
    protected float _baseArmorPenetrationReduction;
    public float BaseArmorPenetrationReduction { get => _baseArmorPenetation; }

    protected float _armorPenetrationReductionModifier;
    public float ArmorPenetrationReductionModifier 
    {
        get => _armorPenetrationReductionModifier;
        set => _armorPenetrationReductionModifier = value;
    }

    public float ArmorPenetrionReducion { get => _baseArmorPenetrationReduction + ArmorPenetrationReductionModifier; }

    [Header("Offensive Stats")]

    //attack power
    [SerializeField] private float _baseAttackPower;
    public float BaseAttackPower { get => _baseAttackPower; }

    protected float _attackPowerModifier = 0f;
    public float AttackPowerModifier {get => _attackPowerModifier; set => _attackPowerModifier = value; }

    public float AttackPower { get => _baseAttackPower + AttackPowerModifier; }

    //armor penetration
    [SerializeField, Tooltip ("The base armor penetration without any modifiers")]
    protected float _baseArmorPenetation;
    public float BaseArmorPenetration { get => _baseArmorPenetation; }

    protected float _armorPenetrationModifier = 0f;
    public float ArmorPenetrationModifier { get => _armorPenetrationModifier; set => _armorPenetrationModifier = value; }

    public float ArmorPenetration { get => BaseArmorPenetration + ArmorPenetrationModifier; }

    //crit
    [SerializeField, Tooltip("Determents if the entity is allowed to Crit")] 
    private bool _canCrit = true;
    public bool CanCrit { get => _canCrit; }

    [SerializeField, Range(0, 100), Tooltip("Determents how high the base crit chance of the entity is")]
    private float _baseCritChance;

    public float CritChanceNormalized { get 
    {
        if ((_baseCritChance + CritChanceModifier) /100f > 1) return 1f;
        else return (_baseCritChance + CritChanceModifier) / 100f; } 
    }

    public float CritChance { get => _baseCritChance + CritChanceModifier; }
    private float _critChanceModifier = 0f;

    public float CritChanceModifier { get => _critChanceModifier; set => _critChanceModifier = value; }

    
    [Header("Entity Types")]
    [SerializeField, Tooltip("Determents the type of the entity")] 
    private eEntityType _entityType;
    public eEntityType EntityType { get => _entityType; }


    protected virtual void Start()
    {
        Health = MaxHealth;
        Armor = MaxArmor;

        OnArmorPercentageChanged?.Invoke(ArmorPercentage);
        OnHealthPercentageChanged?.Invoke(HealthPercentageNormalized);
    }

    /// <summary>
    /// Method is called when the enities healthpool falls to 0
    /// </summary>
    protected virtual void Death()
    {
        //Destroy(this.gameObject);
    }

    /// <summary>
    /// Method is called if the entity loses all his armor 
    /// </summary>
    protected virtual void ArmorBroken()
    {
        if (_isArmorBroken) return;
        _isArmorBroken = true;
        OnArmorBreak?.Invoke();
    }

    /// <summary>
    /// Method is called if the entity restores his armor and now has an armor pool greater then 0
    /// </summary>
    protected virtual void ArmorRestored()
    {
        if (!_isArmorBroken) return;
        _isArmorBroken = false;
        OnArmorRestored?.Invoke();
    }

    [Header("Editor Debugging")]
    [SerializeField] private float _testAttackPower;
    [SerializeField] private float _testArmorPenetration;

    public void TestDealDamage()
    {
        DealDamage(_testAttackPower, _testArmorPenetration, false, 0f);
    }


    public virtual void DealDamage(float attackPower, float armorPenetration, bool canCrit, float critChance)
    {
        float damageToHealth = 0f;
        float damageToArmor = 0f;
        float critModifier = 1f;
        bool didCrit = false;

        if (canCrit)
        {
            if (critChance > UnityEngine.Random.Range(0f, 1f)) 
            {
                critModifier = 2f; 
                didCrit = true;
            }
        }

        if (_isArmorBroken)
        {
            //calculate damage to armor if DamageReduction is bigger then the attackPower, damage is set to 1
            if (attackPower - DamageReduction < 1) damageToHealth = 1f;
            //if DamageReduction isnt bigger then attackPower then calculate normal damage
            else damageToHealth = (attackPower - DamageReduction) * critModifier;
        }
        else
        {
            //calculate damage to armor if ArmorPenetrionReducion is bigger then the armorPenetration, damage is set to 1
            if (armorPenetration - ArmorPenetrionReducion < 1) damageToArmor = 1f;
            //if ArmorPenetrionReducion isnt bigger then armorPenetration then calculate normal damage
            else damageToArmor = (attackPower + (armorPenetration - ArmorPenetrionReducion)) * critModifier;

            //if the damage to the armor is bigger then the actual armor then leftover damage gets subtracted from the health
            if (damageToArmor > Armor) damageToHealth = damageToArmor - Armor;
        }
        //subtract damage to the armor and trigger event
        Armor -= damageToArmor;
        if(damageToArmor > 0) OnArmorDamageTaken?.Invoke(damageToArmor, didCrit);

        //subtract damage to healt and trigger event
        Health -= damageToHealth;
        if (damageToHealth > 0) OnHealthDamageTaken?.Invoke(damageToHealth, didCrit);
    }

    public eEntityType GetEntityType()
    {
        return EntityType;
    }
}