
//TODO: Steuerung nach Richtung. usw

using Assets.Scripts.Player.Relay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour, PlayerProps
    {
        [SerializeField] float mTurnSpeed = .5f;
        [SerializeField] float mMoveSpeedMax = 5f;
        [SerializeField] float mMoveAccel = 10f;
        [SerializeField] float mMoveDrag = 3f;
        [SerializeField] bool isCamFollow = true;
        [SerializeField] Transform mAngleHlp;

        //CharacterController cControl;

        Vector2 moveInput;
        Vector2 currMove = Vector2.zero;

        Vector2 mousPos;

        IShoot[] mGuns;

        float deltaT;
        float lastDir = 0f;

        Rigidbody2D rBody;
        Transform cam;

        private void Awake()
        {
            ReferenceLib.sPlayerCtrl = this;
            //cControl = GetComponent<CharacterController>();
            rBody = GetComponent<Rigidbody2D>();
            mGuns = GetComponentsInChildren<IShoot>();
            cam = Camera.main.transform;
        }
        private void Update()
        {
            CameraZoom();
            DoWeapons();
        }
        private void FixedUpdate()
        {
            deltaT = Time.fixedDeltaTime;
            mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            DoMovement();

            if(isCamFollow)
                cam.position = new Vector3(transform.position.x, transform.position.y, -10);
        }

        void DoMovement()
        {
            UpdateRotation();
            UpdateMoveInput();
            UpdateMovement();
        }
        void UpdateMoveInput()
        {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            moveInput.Normalize();
        }

        void UpdateMovement()
        {
            if (moveInput == Vector2.zero)
                ApplyDrag();
            else
            {
                currMove += moveInput * mMoveAccel * deltaT;
            }

            if (currMove == Vector2.zero) return;

            rBody.MovePosition(rBody.position + currMove * deltaT);
        }
        void ApplyDrag()
        {
            if (currMove == Vector2.zero)
                return;

            if (currMove.sqrMagnitude <= mMoveSpeedMax/10)
            {
                currMove = Vector2.zero;
                return;
            }

            currMove = currMove - currMove * mMoveDrag * deltaT;
        }

        
        void UpdateRotation()
        {
            Vector2 lookDir = mousPos - rBody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            //+anglediff speed

            angle = AngleWrap(Mathf.LerpAngle(lastDir, angle, deltaT / mTurnSpeed));
            lastDir = angle;
            rBody.rotation = angle;
        }
        float AngleWrap(float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle; // > 360 ? 0 : _angle;
        }

        Vector2 TransformToV2(Transform _transform)
        {
            return new Vector2(_transform.position.x, _transform.transform.position.y);
        }
        float GetAngleToVector2(Vector2 _position)
        {
            return Mathf.Atan2(_position.x, _position.y) * Mathf.Rad2Deg - 90f;
        }
        public float AngleDifferenceToTarget(Transform _target, bool _isAbsolut)
        {
            Vector2 target = TransformToV2(_target);
            float ownAngle = GetAngleToVector2(TransformToV2(mAngleHlp)-TransformToV2(transform)) -180;
            float angleToTarget = GetAngleToVector2(target - TransformToV2(transform)) -180;
            float AngleDiff = ownAngle - angleToTarget;

            return _isAbsolut ? Mathf.Abs(AngleDiff) : AngleDiff;
        }
        void CameraZoom()
        {
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;
        }

        void DoWeapons()
        {
            if (Input.GetButton("Fire1"))
                foreach (IShoot gun in mGuns)
                    gun.Fire();
            //if(Input.GetButtonUp("Fire1"))
            //    foreach (IShoot gun in mGuns)
            //        gun.StopFire();
        }
    }

    interface IShoot
    {
        public void Fire();
        public void StopFire();
    }
}