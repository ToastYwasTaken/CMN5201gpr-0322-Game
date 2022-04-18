//Temporär
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class Weapon : MonoBehaviour, IShoot
    {
        [SerializeField] private GameObject lightGO;

        [SerializeField] private bool canShoot = false;
        [SerializeField] private float resetTime;

        [SerializeField] private GameObject mProjectilePfab;

        [SerializeField] private Transform Muzzle;
        private bool doesReset = false;

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

        private void turnOff() => lightGO.SetActive(false);

        private void DoReset()
        {
            canShoot = true;
            doesReset = false;
        }

        private void LightBurst()
        {
            //coroutine .....................
        }
    }
}