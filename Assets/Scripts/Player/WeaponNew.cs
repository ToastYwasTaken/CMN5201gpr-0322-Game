using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class WeaponNew : MonoBehaviour, IShoot
    {
        GameObject Projectile;
        float fireInterval;
        bool canShoot;
        bool isFiring;
        public virtual void Fire() => isFiring = true;
        public void StopFire() => isFiring = false;
        //protected virtual void ExecuteFire()
        protected virtual IEnumerator ExecuteFire()
        {
            if(isFiring)
            {
                if(canShoot)
                {

                }
                else
                {
                    yield return new WaitForSeconds(fireInterval * Time.deltaTime);
                    canShoot = true;
                }
            }
            //Stop
        }
    }
}
