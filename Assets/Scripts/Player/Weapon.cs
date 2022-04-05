//Temporär
using Assets.Scripts.Player;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class Weapon : MonoBehaviour, IShoot
    {
        [SerializeField] GameObject lightGO;

        [SerializeField] bool canShoot = false;
        [SerializeField] float resetTime;

        [SerializeField] GameObject mProjectilePfab;

        [SerializeField] Transform Muzzle;

        bool doesReset = false;

        public void Fire()
        {
            if (doesReset) return;
            if (!canShoot)
            {
                Invoke("DoReset", resetTime);
                doesReset = true;
                return;
            }
            lightGO.SetActive(true);
            Invoke("turnOff", 0.2f);
            canShoot = false;
            Instantiate(mProjectilePfab, Muzzle);
        }
        public void StopFire() { }
        void turnOff() => lightGO.SetActive(false);
        void DoReset()
        {
            canShoot = true;
            doesReset = false;
        }

        void LightBurst()
        {
            //coroutine .....................
        }
    }
}