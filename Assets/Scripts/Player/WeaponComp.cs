using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class WeaponComp : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject _ProjectilePfab;
        [SerializeField] private Transform _Muzzle;
        [SerializeField] private float _ResetTime;
        private bool canShoot = true;

        public void Fire()
        {
            if (!canShoot) return;
            GameObject projectile = Instantiate(_ProjectilePfab, _Muzzle);
            projectile.transform.parent = null;
            canShoot = false;
            StartCoroutine(ShootTimer());
        }

        private IEnumerator ShootTimer()
        {
            yield return new WaitForSeconds(_ResetTime);
            canShoot = true;
        }
    }
}
