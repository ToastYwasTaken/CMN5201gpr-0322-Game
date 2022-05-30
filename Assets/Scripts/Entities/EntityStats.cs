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
    [SerializeField] private float _baseMaxHealth;
    private float BaseMaxHealth { get => _baseMaxHealth; }
    private float _maxHealthModifier;

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

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            float newHealth = value;

            if (newHealth < 0) newHealth = 0;
            if (newHealth > MaxHealth) newHealth = MaxHealth;

            OnHealthChanged?.Invoke(newHealth);
            OnHealthPercentageChanged?.Invoke(HealthPercentageNormalized);

            if (newHealth == 0) Death();

            _health = newHealth;
        }
    }

    public float HealthPercentageNormalized { get => Health / MaxHealth *100f; }
    public float HealthPercentage { get => Health / MaxHealth; }

    // Armor
    [SerializeField] private float _baseMaxArmor;
    public float BaseMaxArmor { get => _baseMaxArmor; }
    private float _armorModifier;
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

    private float _armor;
    public float Armor
    {
        get => _armor;
        set
        {
            float newArmor = value;

            if (newArmor < 0) newArmor = 0;
            if (newArmor > MaxArmor) newArmor = MaxArmor;

            if (_isArmorBroken && newArmor > 0) ArmorRestored();

            OnArmorChanged?.Invoke(newArmor);
            OnArmorPercentageChanged?.Invoke(ArmorPercentage);

            if (newArmor == 0) ArmorBroken();

            _armor = newArmor;
        }
    }

    

    public float ArmorPercentage { get => (_armor / MaxArmor) * 100f; }

    private bool _isArmorBroken = false;
    public bool IsArmorBroken { get => _isArmorBroken; }

    // Damage Reduction
    [SerializeField] private float _baseDamageReduction;
    public float BaseDamageReduction { get => _baseDamageReduction; }
    
    private float _damageReductionModifier;
    public float DamageReductionModifier 
    { 
        get => _damageReductionModifier;
        set => _damageReductionModifier = value;
    }

    public float DamageReduction { get => _baseDamageReduction + DamageReductionModifier; }

    // Armor Penetration Reduction
    [SerializeField] private float _baseArmorPenetrationReduction;
    public float BaseArmorPenetrationReduction { get => _baseArmorPenetation; }
    private float _armorPenetrationReductionModifier;
    public float ArmorPenetrationReductionModifier 
    {
        get => _armorPenetrationReductionModifier;
        set => _armorPenetrationReductionModifier = value;
    }
    public float ArmorPenetrionReducion { get => _baseArmorPenetrationReduction + ArmorPenetrationReductionModifier; }

    [Header("Offensive Stats")]

    [SerializeField] private float _baseAttackPower;
    public float BaseAttackPower { get => _baseAttackPower; }
    private float _attackPowerModifier = 0f;
    public float AttackPowerModifier {get => _attackPowerModifier; set => _attackPowerModifier = value; }
    public float AttackPower { get => _baseAttackPower + AttackPowerModifier; }

    [SerializeField]private float _baseArmorPenetation;
    public float BaseArmorPenetration { get => _baseArmorPenetation; }
    private float _armorPenetrationModifier = 0f;
    public float ArmorPenetrationModifier { get => _armorPenetrationModifier; set => _armorPenetrationModifier = value; }
    public float ArmorPenetration { get => _baseArmorPenetation + ArmorPenetrationModifier; }

    [SerializeField] private bool _canCrit = true;
    public bool CanCrit { get => _canCrit; }

    [SerializeField, Range(0, 100)] private float _baseCritChance;
    public float CritChanceNormalized { get => (_baseCritChance + CritChanceModifier)/ 100f; }
    public float CritChance { get => _baseCritChance + CritChanceModifier; }
    private float _critChanceModifier = 0f;
    public float CritChanceModifier { get => _critChanceModifier; set => _critChanceModifier = value; }

    [Header("Entity Types")]
    [SerializeField] private eEntityType _entityType;
    public eEntityType EntityType { get => _entityType; }


    private void Start()
    {
        Health = MaxHealth;
        Armor = MaxArmor;

        OnArmorPercentageChanged?.Invoke(ArmorPercentage);
        OnHealthPercentageChanged?.Invoke(HealthPercentageNormalized);
    }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        //Destroy(this.gameObject);
    }

    protected virtual void ArmorBroken()
    {
        if (_isArmorBroken) return;
        _isArmorBroken = true;
        OnArmorBreak?.Invoke();
    }

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
            damageToHealth = (attackPower - _baseDamageReduction) * critModifier;
        }
        else
        {
            if (armorPenetration - _baseArmorPenetrationReduction < 1) damageToArmor = 0f;
            else damageToArmor = (attackPower + (armorPenetration - _baseArmorPenetrationReduction)) * critModifier;

            if (damageToArmor > _armor) damageToHealth = damageToArmor - _armor;
        }

        Armor -= damageToArmor;
        if(damageToArmor > 0) OnArmorDamageTaken?.Invoke(damageToArmor, didCrit);
        Health -= damageToHealth;
        if (damageToHealth > 0) OnHealthDamageTaken?.Invoke(damageToHealth, didCrit);
    }

    public eEntityType GetEntityType()
    {
        return EntityType;
    }
}