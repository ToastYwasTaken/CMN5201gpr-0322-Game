using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{

    public struct ProjectileStats
    {

        public float AttackPower;
        public float ArmorPenetration;
        public float ProjectileSpeed;

        public float ProjectileLifeTime;
    }

    class Projectile : MonoBehaviour
    {
        private ProjectileStats _projectileStats;
        public ProjectileStats ProjectileStats { get => _projectileStats;
                                                 set { _projectileStats = value; }
        }

        [SerializeField] private Rigidbody2D _rb;


        private void Start()
        {
            if (_rb == null) _rb = GetComponent<Rigidbody2D>();   

            _rb.velocity = Vector2.zero;

            _rb.AddForce(transform.up * _projectileStats.ProjectileSpeed, ForceMode2D.Impulse);

            Destroy(gameObject, _projectileStats.ProjectileLifeTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DamageTarget(collision.gameObject);
            Destroy(gameObject);
        }

        private void DamageTarget(GameObject target)
        {
            IHealth targetIHealth = target.GetComponent<IHealth>();
            targetIHealth?.ChangeHealth(_projectileStats.AttackPower);
        }
    }
}
