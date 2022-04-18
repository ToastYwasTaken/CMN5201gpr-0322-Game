public class PlayerStats
{
    public delegate void ChangedHealth(float newHealth);
    public event ChangedHealth OnHealthChanged;
    public event ChangedHealth OnHealthPercentageChanged;

    private float _health;
    public float Heath { 
        get => _health;
        set
        {
            float newHealth = value;

            if (newHealth < 0) newHealth = 0;
            if (newHealth > _maxHealth) newHealth = _maxHealth;

            OnHealthChanged?.Invoke(newHealth);
            OnHealthPercentageChanged?.Invoke(HealthPercentage);

            _health = newHealth;
        }
    }

    private float _maxHealth;
    public float MaxHealth { get => _maxHealth; }

    private float HealthPercentage { get => _health / _maxHealth; }

    private float _attackPower;
    public float AttackPower { get => _attackPower; }
    
    private float _armorPenetation;
    public float ArmorPenetration { get => _armorPenetation; }


    private float _damageReduction;
    public float DamageReduction { get => _damageReduction; }
    
    private float _armorPenetrationReduction;
    public float ArmorPenetrionReducion { get => _armorPenetrationReduction; }

    public PlayerStats(float maxHealth, 
                       float attackPower, 
                       float armorPenetration,
                       float damageReduction, 
                       float armorPenetraionReduction)
    {
        Heath = maxHealth;
        _maxHealth = maxHealth;

        _attackPower = attackPower;
        _armorPenetation = armorPenetration;
        _damageReduction = damageReduction;
        _armorPenetrationReduction = armorPenetraionReduction;
    }
}
