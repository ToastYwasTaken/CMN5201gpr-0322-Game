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

    [SerializeField] private float _maxHealth;
    public float MaxHealth { get => _maxHealth; }

    private float _health;
    public float Health
    {
        get => _health;
        set
        {
            float newHealth = value;

            if (newHealth < 0) newHealth = 0;
            if (newHealth > _maxHealth) newHealth = _maxHealth;

            OnHealthChanged?.Invoke(newHealth);
            OnHealthPercentageChanged?.Invoke(HealthPercentage);

            if (newHealth == 0) Death();

            _health = newHealth;
        }
    }

    public float HealthPercentage { get => (_health / _maxHealth) *100f; }

    [SerializeField] private float _maxArmor;
    public float MaxArmor { get => _maxArmor; }

    private float _armor;
    public float Armor
    {
        get => _armor;
        set
        {
            float newArmor = value;

            if (newArmor < 0) newArmor = 0;
            if (newArmor > _maxArmor) newArmor = _maxArmor;

            if (_isArmorBroken && newArmor > 0) ArmorRestored();

            OnArmorChanged?.Invoke(newArmor);
            OnArmorPercentageChanged?.Invoke(ArmorPercentage);

            if (newArmor == 0) ArmorBroken();

            _armor = newArmor;
        }
    }

    

    public float ArmorPercentage { get => (_armor / _maxArmor) * 100f; }

    private bool _isArmorBroken = false;
    public bool IsArmorBroken { get => _isArmorBroken; }


    [SerializeField] private float _damageReduction;
    public float DamageReduction { get => _damageReduction; }
    
    [SerializeField] private float _armorPenetrationReduction;
    public float ArmorPenetrionReducion { get => _armorPenetrationReduction; }


    [SerializeField] private float _attackPower;
    public float AttackPower { get => _attackPower; }
    
    [SerializeField]private float _armorPenetation;
    public float ArmorPenetration { get => _armorPenetation; }

    [SerializeField] private eEntityType _entityType;
    public eEntityType EntityType { get => _entityType; }


    private void Start()
    {
        Health = _maxHealth;
        Armor = _maxArmor;

        OnArmorPercentageChanged?.Invoke(ArmorPercentage);
        OnHealthPercentageChanged?.Invoke(HealthPercentage);
    }

    protected virtual void Death()
    {
        OnDeath?.Invoke();
        Destroy(this.gameObject);
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

    [SerializeField] private float _testAttackPower;
    [SerializeField] private float _testArmorPenetration;

    public void TestDealDamage()
    {
        DealDamage(_testAttackPower, _testArmorPenetration);
    }


    public virtual void DealDamage(float attackPower, float armorPenetration)
    {
        float damageToHealth = 0f;
        float damageToArmor = 0f;

        if (_isArmorBroken)
        {
            damageToHealth = attackPower - _damageReduction;
        }
        else
        {
            if (armorPenetration - _armorPenetrationReduction < 1) damageToArmor = 0f;
            else damageToArmor = (attackPower * (armorPenetration - _armorPenetrationReduction));

            if (damageToArmor > _armor) damageToHealth = damageToArmor - _armor;
        }

        Debug.Log($"Damage to armor : {damageToArmor} | Damage to health {damageToHealth} ");
        Armor -= damageToArmor;
        Health -= damageToHealth;
    }

    public eEntityType GetEntityType()
    {
        return EntityType;
    }
}
