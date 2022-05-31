// 22.4.22 JA created 

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
        [SerializeField] Moveable _movable;
        [HideInInspector] Rotateable _rotateable;
        [HideInInspector] CameraController _cameraController;
        [SerializeField] Rotateable _leftWpn, _rightWpn;
        Inventory _inventory;
        public Inventory Inventory { get => _inventory; }
        private void Awake()
        {
            RefLib.Player = gameObject;
            RefLib.sPlayerCtrl = this;
            if (_movable == null)_movable = GetComponent<Moveable>();
            if (_rotateable == null) _rotateable = GetComponent<Rotateable>();
            if (_cameraController == null) _cameraController = GetComponent<CameraController>();
            //if (_inventory == null) _inventory = new Inventory(10, GetComponent<WeaponManager>());
        }
        private void Update()
        {
            if (!GlobalValues.IsPlayerActive) return;
            _cameraController.DoCamera();
            //DoInput
            _movable.DoMovement();
        }

        private void FixedUpdate()
        {
            if (!GlobalValues.IsPlayerActive) return;
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _rotateable.RotateTowardsTargetV2(mousePos);
            _leftWpn.RotateTowardsTargetV2(mousePos);
            _rightWpn.RotateTowardsTargetV2(mousePos);
            
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
