// 22.4.22 JA created 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class CameraController : MonoBehaviour
    {
        Transform _camera;

        private void Awake()
        {
            _camera = Camera.main.transform;
        }

        public void DoCamera()
        {
            CameraMovement();
            CameraZoom();
        }

        private void CameraMovement()
        {
            _camera.position = new Vector3(transform.position.x, transform.position.y, -10);
        }
        private void CameraZoom()
        {
            Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * 2;
        }
    }
}
