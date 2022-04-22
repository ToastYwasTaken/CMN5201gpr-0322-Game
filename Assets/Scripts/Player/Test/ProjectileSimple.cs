using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class ProjectileSimple : MonoBehaviour
    {
        [SerializeField] private float mLifeTime;
        [SerializeField] private float mSpeed = 5f;
        [SerializeField] private float damage = -5f;
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
            DamageTarget(collision.gameObject);

            //ContactPoint2D contact = collision.GetContact(0); //collision.contacts[0];

            //Quaternion rotation =
            //    Quaternion.FromToRotation(Vector3.up, contact.normal);

            //Vector2 position = contact.point;

            //Instantiate(mExplosionPrefab, new Vector3(position.x, position.y, 0), rotation);
            Destroy(gameObject);
        }
        private void DamageTarget(GameObject target)
        {
            IHealth targetIHealth = target.GetComponent<IHealth>();
            targetIHealth?.ChangeHealth(damage);
        }
    }
}
