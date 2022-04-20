using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class WeaponNew : MonoBehaviour, IWeapon
    {
        private GameObject Projectile;
        private float fireInterval;
        private bool canShoot;
        private bool isFiring;
        public virtual void Fire() => isFiring = true;
        public void StopFire() => isFiring = false;
        //protected virtual void ExecuteFire()
        protected virtual IEnumerator ExecuteFire()
        {
            if (isFiring)
            {
                if (canShoot)
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
