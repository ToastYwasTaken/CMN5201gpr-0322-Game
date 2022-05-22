using UnityEngine;

namespace Assets.Scripts.Player
{
    public class Damagable : MonoBehaviour, IHealth, IEntity
    {
        [SerializeField] private float mHealth;
        [SerializeField] eEntityType _eType;
        [SerializeField] private float mCurrentHealth;

        private void Awake()
        {
            mCurrentHealth = mHealth;
        }
        public void ChangeHealth(float _amount)
        {
            mCurrentHealth -= _amount;
            if (mCurrentHealth < 0)
            {
                mCurrentHealth = 0;
                OnDeath();
            }
            //event health changed ?
        }
        public float DamageAmount()
        {
            return mCurrentHealth / mHealth;
        }
        public eEntityType EType()
        {
            return _eType;
        }

        protected virtual void OnDeath()
        {
        }
    }

    internal interface IHealth
    {
        public void ChangeHealth(float _amount);
        public eEntityType EType();
    }
    internal interface IEntity
    {
        public eEntityType EType();
    }
}
