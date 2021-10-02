using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Cameras
{
    public class CameraController : MonoBehaviour
    {
        public Transform Target;
        public float Height = 5f;
        public float Distance = 30f;

        private float _pitch, _yaw;

        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        void Update()
        {
            _yaw += Input.GetAxis("Mouse X");
            _pitch += Input.GetAxis("Mouse Y");

            _pitch = Mathf.Clamp(_pitch, -80f, 89f);

            var rotation = Quaternion.Euler(_pitch, _yaw, 0f);

            transform.rotation = rotation;
            transform.position =
                Target.position +
                (Vector3.up * Height) +
                (rotation * Vector3.back) * Distance;
        }
    }
}