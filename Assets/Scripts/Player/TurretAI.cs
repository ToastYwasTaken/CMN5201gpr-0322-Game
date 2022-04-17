using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(WeaponComp))]
    [RequireComponent(typeof(Rotateable))]
    internal class TurretAI : MonoBehaviour
    {
        [SerializeField] private Transform target;
        private Rotateable _Rotateable;
        private WeaponComp _Weapon;

        [SerializeField] private bool isActive;
        [SerializeField] private bool isShoot;
        private bool isOnTarget = false;

        private void Awake()
        {
            _Rotateable = GetComponent<Rotateable>();
            _Weapon = GetComponent<WeaponComp>();
        }
        private void Start()
        {
            StartCoroutine(AILoop());
            StartCoroutine(OnTargetLoop());
        }

        private IEnumerator AILoop()
        {
            while (isActive)
            {
                while (!isOnTarget)
                {
                    _Rotateable.RotateTowardsTarget(target);
                    yield return Time.fixedDeltaTime;
                }
                while (isOnTarget && isShoot)
                {
                    _Weapon.Fire();
                    yield return Time.fixedDeltaTime;
                }
                yield return Time.fixedDeltaTime;
            }
        }

        private IEnumerator OnTargetLoop()
        {
            while (isActive)
            {
                float targetAngleDiff = _Rotateable.AngleDifferenceToTarget(target, true);
                isOnTarget = targetAngleDiff < 2;
                yield return Time.fixedDeltaTime;
            }
        }
    }
}
