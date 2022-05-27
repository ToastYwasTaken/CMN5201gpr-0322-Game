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
        float _input;

        private void Awake()
        {
            _camera = Camera.main.transform;
            _input = Camera.main.orthographicSize;
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

        [SerializeField] float _zoomMin = 5f, _zoomMax = 8f;
        private void CameraZoom()
        {
            _input -= Input.GetAxis("Mouse ScrollWheel") * 2;
            _input = Math.Clamp(_input, _zoomMin, _zoomMax);
            Camera.main.orthographicSize = _input;
        }
    }
}
