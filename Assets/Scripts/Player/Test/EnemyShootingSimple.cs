/*****************************************************************************
* Project: CMN5201gpr-0322-Game
* File   : Rotateable.cs
* Date   : 17.04.22
* Author : Jan Apsel (JA)
*
* These coded instructions, statements, and computer programs contain
* proprietary information of the author and are protected by Federal
* copyright law. They may not be disclosed to third parties or copied
* or duplicated in any form, in whole or in part, without the prior
* written consent of the author.
*
* History:
*   22.4.22 JA created 
******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AngleExtension;
namespace Assets.Scripts.Player 
{
    public class EnemyShootingSimple : MonoBehaviour
    {
        [SerializeField] Transform _parent;
        private float _ownAngle;
        [SerializeField] float _range, _aimRange;
        Transform _target;
        [SerializeField] bool IsRaycastActive;

        [HideInInspector]IWeapon _weapon;

        private void Awake()
        {
            if(_weapon == null) _weapon = GetComponent<IWeapon>();
        }
        private void Start()
        {
            _target = RefLib.sPlayer.transform;
        }
        private void FixedUpdate()
        {
            if (_target == null) return;
            if ( IsInRange(_target) && IsAimingAt(_target) && (IsRaycastActive ? IsNotObstructed(_target) : true))
            {
                _weapon.Fire();
            }
        }
        bool IsInRange(Transform target)
        {
            float sqrMag = (_parent.position.ToVector2() - target.position.ToVector2()).sqrMagnitude;
            return sqrMag < _range;
        }
        bool IsNotObstructed(Transform target)
        {
            Vector3 targetV3 = new Vector3(target.position.x, target.position.y, 0);
            Vector3 selfV3 = new Vector3(_parent.position.x, _parent.position.y, 0);
            RaycastHit hit;
            Physics.Raycast(selfV3, (targetV3 -selfV3).normalized * 100, out hit);
            if (hit.collider != null)
            {
                Debug.DrawRay(selfV3, (targetV3 -selfV3).normalized * 100, Color.white);
                IEntity iEtt = hit.transform.gameObject.GetComponent<IEntity>();
                if (iEtt != null)
                {
                    if (iEtt.EType() == eEntityType.Player)
                        return true;
                }
            }
            return false;
        }
        bool IsAimingAt(Transform target)
        {
            if (AngleDifferenceToTarget(target, true) < _aimRange)
                return true;
            return false;
        }
        public float AngleDifferenceToTarget(Transform target, bool isAbsolut)
        {
            float ownAngle = _parent.up.ToVector2().GetAngle();
            float angleToTarget = (target.ToVector2() - _parent.position.ToVector2()).GetAngle();
            float angleDiff = ownAngle - angleToTarget;
            if (angleDiff > 180) angleDiff = -180 + angleDiff % 180;

            return isAbsolut ? Mathf.Abs(angleDiff) : angleDiff;
        }
    }
}