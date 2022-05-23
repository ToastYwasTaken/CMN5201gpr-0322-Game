using UnityEngine;
using AngleExtension;


namespace Assets.Scripts.Player
{
    internal class ProjectileSimple : MonoBehaviour
    {
        [SerializeField] private float mLifeTime;
        [SerializeField] private float mSpeed = 5f;
        [SerializeField] private float damage = -5f;

        [SerializeField] private float _armorPenetration;
        [SerializeField] private float _attackPower;
        //[SerializeField] private float health = 5f;

        //[SerializeField] private GameObject mExplosionPrefab;
        [SerializeField] private Rigidbody2D mRigidbody;

        //private void Awake()
        //{
        //    mRigidbody = GetComponent<Rigidbody>();
        //}

        private void Start()
        {
            mRigidbody.velocity = Vector2.zero;

            mRigidbody.AddForce(transform.up * mSpeed, ForceMode2D.Impulse);

            Destroy(gameObject, mLifeTime);
        }

        public float Damage()
        {
            return damage;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DamageTarget(collision.GetContact(0).collider.gameObject);
            //print(collision.GetContact(0).collider.gameObject.name);

            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();

            if (damageable != null) 
                if (collision.gameObject.GetComponent<PlayerController2>())
                {
                    damageable.DealDamage(_attackPower, _armorPenetration, false, 0f);
                    Destroy(gameObject);
                }


            //DamageTargetArea(collision.gameObject);


            //ContactPoint2D contact = collision.GetContact(0); //collision.contacts[0];

            //Quaternion rotation =
            //    Quaternion.FromToRotation(Vector3.up, contact.normal);

            //Vector2 position = contact.point;

            //Instantiate(mExplosionPrefab, new Vector3(position.x, position.y, 0), rotation);
            Destroy(gameObject);
        }
        private void DamageTargetArea(GameObject target)
        {
            IDmgSegment dmgSegment = target.GetComponent<IDmgSegment>();
            dmgSegment?.DmgByPosition(transform, damage);
        }
        private void DamageTarget(GameObject target)
        {
            IHealth targetIHealth = target.GetComponent<IHealth>();
            targetIHealth?.ChangeHealth(damage);
        }
    }
}
