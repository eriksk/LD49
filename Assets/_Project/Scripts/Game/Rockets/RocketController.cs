using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets
{
    public class RocketController : MonoBehaviour
    {
        public float EngineForce = 1500f;
        public float ThrusterForce = 500f;
        public ThrusterReferences Thrusters;

        private Rigidbody _rigidbody;

        private ThrusterState _thrusterState;
        private float _engineState;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            _engineState = Input.GetMouseButton(0) ? 1f : 0f;
            _thrusterState = new ThrusterState()
            {
                Left = Input.GetKey(KeyCode.A) ? 1f : 0f,
                Right = Input.GetKey(KeyCode.D) ? 1f : 0f,
                Front = Input.GetKey(KeyCode.W) ? 1f : 0f,
                Back = Input.GetKey(KeyCode.S) ? 1f : 0f,
            };
        }

        void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * _engineState * EngineForce);

            _rigidbody.AddForceAtPosition(Thrusters.Left.up * _thrusterState.Left * ThrusterForce, Thrusters.Left.position);
            _rigidbody.AddForceAtPosition(Thrusters.Right.up * _thrusterState.Right * ThrusterForce, Thrusters.Right.position);
            _rigidbody.AddForceAtPosition(Thrusters.Front.up * _thrusterState.Front * ThrusterForce, Thrusters.Front.position);
            _rigidbody.AddForceAtPosition(Thrusters.Back.up * _thrusterState.Back * ThrusterForce, Thrusters.Back.position);
        }
    }

    public struct ThrusterState
    {
        public float Left, Right, Front, Back;
    }

    [System.Serializable]
    public class ThrusterReferences
    {
        public Transform Left, Right, Front, Back;
    }
}