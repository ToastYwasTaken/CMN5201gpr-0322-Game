using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(WeaponComp))]
    [RequireComponent(typeof(Rotateable))]
    class TurretAI : MonoBehaviour
    {
        [SerializeField]Transform target;
        Rotateable _Rotateable;
        WeaponComp _Weapon;

        [SerializeField] bool isActive;
        bool isOnTarget = false;

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

        IEnumerator AILoop()
        {
            while(isActive)
            {
                while(!isOnTarget)
                {
                    _Rotateable.RotateToTarget(target);
                    yield return Time.fixedDeltaTime;
                }
                while(isOnTarget)
                {
                    _Weapon.Fire();
                    yield return Time.fixedDeltaTime;
                }
                yield return Time.fixedDeltaTime;
            }
        }
        IEnumerator OnTargetLoop()
        {
            while(isActive)
            {
                float targetAngleDiff = _Rotateable.AngleDifferenceToTarget(target, true);
                isOnTarget = targetAngleDiff < 2;
                yield return Time.fixedDeltaTime;
            }
        }
    }
}
