using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class Damagable : MonoBehaviour, IHealth
    {
        [SerializeField] private float mHealth;
        private float mCurrentHealth;

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
        protected virtual void OnDeath()
        {
        }
    }

    internal interface IHealth
    {
        public void ChangeHealth(float _amount);
    }
}
