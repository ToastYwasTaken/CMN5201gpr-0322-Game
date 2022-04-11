using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    class WeaponComp : MonoBehaviour
    {
        [SerializeField] GameObject _ProjectilePfab;
        [SerializeField] Transform _Muzzle;
        [SerializeField] float _ResetTime;

        bool canShoot = true;

        public void Fire()
        {
            if (!canShoot) return;
            Instantiate(_ProjectilePfab, _Muzzle);
            StartCoroutine(ShootTimer());
        }

        IEnumerator ShootTimer()
        {
            canShoot = false;
            yield return new WaitForSeconds(_ResetTime * Time.deltaTime * 20);
            canShoot = true;
        }
    }
}
