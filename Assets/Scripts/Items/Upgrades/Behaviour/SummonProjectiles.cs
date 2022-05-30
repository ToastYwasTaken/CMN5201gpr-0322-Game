using UnityEngine;

[CreateAssetMenu(fileName = "New Behaviour", menuName = "Items/Overdrive_Chips/Behaviour/Summon_Projectile", order = 100)]
public class SummonProjectiles : OverdriveBehaviour
{
    [Header("Summon Configuration")]
    [SerializeField] private int _amountOfBullets;
    [SerializeField] private float _angle;
    [SerializeField] private bool _randomAngle;

    [Header("Offensive Stats")]
    [SerializeField] float _weaponPower;
    [SerializeField] float _armorPenetration;

    [Header("Bullet Configuration")]
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _bulletSpeed;
    [SerializeField] float _projectileLifetime;

    public override void UseOverdriveEffect(PlayerInformation playerInformation)
    {
        GameObject firePoint = playerInformation.WeaponManager.gameObject;
        ProjectileStats projectileStats = SetupProjectileStats(playerInformation.PlayerStats);


        if (_randomAngle)
        {
            for (int i = 0; i < _amountOfBullets; i++)
            {
                GameObject newBullet = Instantiate(_bulletPrefab, firePoint.transform);
                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + _angle /2f;
                float angleIncrease = Random.Range(0f, _angle);

                float tempRot = startRoation - angleIncrease * i;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot));
                newBullet.transform.localScale = Vector3.one;

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(null);
            }
        }
        else
        {
            for (int i = 0; i < _amountOfBullets; i++)
            {
                GameObject newBullet = Instantiate(_bulletPrefab, firePoint.transform);
                float facingRotation = newBullet.transform.rotation.z;

                float startRoation = facingRotation + _angle /2f;
                float angleIncrease = _angle / ((float)_amountOfBullets -1);

                float tempRot = startRoation - angleIncrease * i;

                newBullet.transform.Rotate(new Vector3(0f, 0f, tempRot));
                newBullet.transform.localScale = Vector3.one;

                newBullet.GetComponent<Projectile>().ProjectileStats = projectileStats;

                newBullet.transform.SetParent(null);
            }
        }
    }

    private ProjectileStats SetupProjectileStats(EntityStats playerStats)
    {
        ProjectileStats newBulletStats = new()
        {
            ArmorPenetration = _armorPenetration,
            AttackPower = DamageCalculation.CalculateAttackPower(playerStats.BaseAttackPower, _weaponPower),

            CanCrit = playerStats.CanCrit,
            CritChance = playerStats.CritChanceNormalized,

            ProjectileOwnerType = playerStats.EntityType,
            ProjectileSender = playerStats.gameObject,

            ProjectileSpeed = _bulletSpeed,
            ProjectileLifeTime = _projectileLifetime
    };
        return newBulletStats;
    }
}