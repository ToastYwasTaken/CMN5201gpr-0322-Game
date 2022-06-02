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
        [SerializeField] ItemSpawner _spawner;
        [HideInInspector] Rotateable _rotateable;
        [HideInInspector] CameraController _cameraController;
        [SerializeField] Rotateable _leftWpn, _rightWpn;
        [SerializeField] DmgFlash _flash;
        [SerializeField] GameObject _menu;
        Inventory _inventory;
        public Inventory Inventory { get => _inventory; }
        private void Awake()
        {
            Time.timeScale = 0;
            GlobalValues.sIsPlayerActive = false;
            RefLib.sPlayer = gameObject;
            RefLib.sPlayerCtrl = this;
            if (_flash == null) gameObject.GetComponentInChildren<DmgFlash>();
            if (_movable == null)_movable = GetComponent<Moveable>();
            if (_rotateable == null) _rotateable = GetComponent<Rotateable>();
            if (_cameraController == null) _cameraController = GetComponent<CameraController>();
            //if (_inventory == null) _inventory = new Inventory(10, GetComponent<WeaponManager>());
        }
        private void Start()
        {
            Invoke("EnableEnemyAggro", 3f);
        }

        public void SpawnPlayer()
        {
            Transform spawn = _spawner.GetPlayerSpawn();
            transform.position = spawn.position;
            GlobalValues.sIsPlayerActive = true;

            GetComponent<EntityStats>().OnDeath += _flash.DeathFlash;
            //_flash.OnRoutineDone += SwitchVictoryMenu;

            Time.timeScale = 1;

            Invoke("EnableEnemyAggro", 3f);
        }
        void EnableEnemyAggro()
        {
            GlobalValues.sIsPlayerActive = true;
        }

        public void SwitchVictoryMenu()
        {
            GlobalValues.sCurrentLevel++;
            GlobalValues.sIsPlayerActive = false;
            Time.timeScale = 0;
            _menu.SetActive(true);
        }
        public void SwitchRestartMenu()
        {
            GlobalValues.sCurrentLevel++;
            GlobalValues.sIsPlayerActive = false;
            Time.timeScale = 0;
            _menu.SetActive(true);
        }
        private void Update()
        {
            if (!GlobalValues.sIsPlayerActive) return;
            _cameraController.DoCamera();
            //DoInput
            _movable.DoMovement();
        }

        private void FixedUpdate()
        {
            if (!GlobalValues.sIsPlayerActive) return;
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
