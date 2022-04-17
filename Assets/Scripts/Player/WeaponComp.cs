using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class WeaponComp : MonoBehaviour
    {
        [SerializeField] private GameObject _ProjectilePfab;
        [SerializeField] private Transform _Muzzle;
        [SerializeField] private float _ResetTime;
        private bool canShoot = true;

        public void Fire()
        {
            if (!canShoot) return;
            Instantiate(_ProjectilePfab, _Muzzle);
            StartCoroutine(ShootTimer());
        }

        private IEnumerator ShootTimer()
        {
            canShoot = false;
            yield return new WaitForSeconds(_ResetTime * Time.deltaTime * 20);
            canShoot = true;
        }
    }
}
