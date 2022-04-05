using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class Damagable : MonoBehaviour, IHealth
    {
        [SerializeField] float mHealth;
        float mCurrentHealth;

        private void Awake()
        {
            mCurrentHealth = mHealth;
        }
        public void ChangeHealth(float _amount)
        {
            mCurrentHealth += _amount;
            if(mCurrentHealth < 0)
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

    interface IHealth
    {
        public void ChangeHealth(float _amount);
    }
}
