using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    internal class WeaponComp : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject _projectilePfab;
        [SerializeField] private Transform[] _muzzles;
        [SerializeField] private float _fireRate, _coolDownTime;
        [SerializeField] private int _shotNum;
        private bool _canShoot = true;
        private int _currentShot = 0;

        private void Awake()
        {
            if (_muzzles.Length < 1) _canShoot = false;
        }
        public void Fire()
        {
            if (!_canShoot) return;

            GameObject projectile = Instantiate(_projectilePfab, NextMuzzle());
            projectile.transform.parent = null;

            if (_coolDownTime > 0)
            {
                _currentShot++;
                if (_currentShot >= _shotNum)
                {
                    _currentShot = 0;
                    _canShoot = false;
                    StartCoroutine(Timer(_coolDownTime));
                    return;
                }
            }
            _canShoot = false;
            StartCoroutine(Timer(_fireRate));
        }

        private IEnumerator Timer(float time)
        {
            yield return new WaitForSeconds(time);
            _canShoot = true;
        }

        int currMuzzle = 0;
        Transform NextMuzzle()
        {
            if (currMuzzle >= _muzzles.Length)
                currMuzzle = 0;
            currMuzzle++;
            return _muzzles[currMuzzle - 1];
        }
    }
}
