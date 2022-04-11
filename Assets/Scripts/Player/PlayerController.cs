
//TODO:
//Steuerung nach Richtung.
//Rotation inerta ;_;

using AngleExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField][Range(0, 50)] float mTurnSpeed = 20f;
        [SerializeField][Range(0, 50)] float mMoveSpeedMax = 15f;
        [SerializeField][Range(0, 50)] float mMoveAccel = 10f;
        [SerializeField][Range(0, 50)] float mMoveDrag = 15f;
        [SerializeField] bool isCamFollow = true;
        [SerializeField] AnimationCurve mRotationCurve;

        float pTurnSpeed { get { return mTurnSpeed * 10; } set{ value = mTurnSpeed; } } 
        float pMoveSpeedMax { get { return mMoveSpeedMax; } set { value = mMoveSpeedMax; } }
        float pMoveAccel { get { return mMoveAccel; } set { value = mMoveAccel; } }
        float pMoveDrag { get { return mMoveDrag /5; } set { value = mMoveDrag; } }  
        //CharacterController cControl;

        Vector2 moveInput;
        [SerializeField] Vector2 mVelocity = Vector2.zero;

        Vector2 mousPos;

        IShoot[] mGuns;

        float deltaT;
        float lastDir = 0f;
        float targetAngle;

        Rigidbody2D rBody;
        Transform cam;

        private void Awake()
        {
            ReferenceLib.sPlayerCtrl = this;
            //cControl = GetComponent<CharacterController>();
            rBody = GetComponent<Rigidbody2D>();
            mGuns = GetComponentsInChildren<IShoot>();
            cam = Camera.main.transform;
            //currTarget = transform.up.ToVector2().GetAngle();

        }
        private void Update()
        {
            CameraZoom();
            DoWeapons();


        }
        private void FixedUpdate()
        {
            mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            deltaT = Time.fixedDeltaTime;
            DoMovement();

            if (isCamFollow)
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
                mVelocity += moveInput * pMoveAccel * deltaT;

            float sqrMag = mVelocity.sqrMagnitude;
            if (sqrMag >= pMoveSpeedMax)
            {
                mVelocity = mVelocity * (pMoveSpeedMax/ sqrMag);
            }

            if (mVelocity == Vector2.zero) return;

            rBody.MovePosition(rBody.position + mVelocity * deltaT);
        }
        void ApplyDrag()
        {
            if (mVelocity == Vector2.zero)
                return;

            mVelocity = mVelocity - mVelocity * pMoveDrag/5 * deltaT;
        }
        

        void UpdateRotation()
        {
            Vector2 lookDir = mousPos - rBody.position;
            float lookAngle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            if(lookAngle != targetAngle)
                targetAngle = LerpAngle(targetAngle, lookAngle);

            float angleDiff = Mathf.DeltaAngle(targetAngle, lastDir);
            if (angleDiff == 0) return;

            float currAngle = AngleWrap(Mathf.LerpAngle(lastDir, targetAngle, LerpDist(angleDiff)));
            rBody.rotation = currAngle;
            lastDir = currAngle;
        }
        float LerpAngle(float currTarget, float newTarget)
        {
            float angleDiff = Mathf.Abs(Mathf.DeltaAngle(currTarget, newTarget));

            return Mathf.LerpAngle(currTarget, newTarget, LerpDist(angleDiff)); /////
        }
        float LerpDist(float _angleDiff)
        {
            _angleDiff = Mathf.Abs(_angleDiff);
            float distUnified = (180 / _angleDiff) /180;
            return Mathf.Clamp01(mRotationCurve.Evaluate(_angleDiff / 180) * distUnified * deltaT * pTurnSpeed);
        }
        float AngleWrap(float _angle)
        {
            return _angle < 0 ? 360 + _angle : _angle > 360 ? 0 : _angle;
        }

        public float AngleDifferenceToTarget(Transform _target, bool _isAbsolut)
        {
            float ownAngle = transform.up.ToVector2().GetAngle();
            float angleToTarget = (_target.ToVector2() - rBody.position).GetAngle();
            float AngleDiff = ownAngle - angleToTarget;
            if (AngleDiff > 180) AngleDiff = -180 + AngleDiff % 180;

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