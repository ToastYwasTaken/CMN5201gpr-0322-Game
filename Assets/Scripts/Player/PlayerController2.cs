using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Moveable))]
    [RequireComponent(typeof(Rotateable))]
    [RequireComponent(typeof(CameraController))]
    public class PlayerController2 : MonoBehaviour
    {
        [HideInInspector] Moveable _movable;
        [HideInInspector] Rotateable _rotateable;
        [HideInInspector] CameraController _cameraController;
        private void Awake()
        {
            if (_movable == null)_movable = GetComponent<Moveable>();
            if (_rotateable == null) _rotateable = GetComponent<Rotateable>();
            if (_cameraController == null) _cameraController = GetComponent<CameraController>();
        }

        private void FixedUpdate()
        {
            _rotateable.RotateTowardsTargetV2(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            _movable.DoMovement();
            _cameraController.DoCamera();
            DoWeapons();
        }

        private IShoot _shoot;
        private void DoWeapons()
        {
            if (_shoot == null) _shoot = GetComponent<IShoot>();
            if (_shoot == null) return;

            if (Input.GetButton("Fire1"))
            {
                _shoot.Shoot();
            }
        }
    }
}
