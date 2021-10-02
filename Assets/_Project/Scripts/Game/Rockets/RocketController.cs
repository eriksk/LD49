using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets
{
    public class RocketController : MonoBehaviour
    {
        public float EngineForce = 1500f;
        public float ThrusterForce = 500f;
        public ThrusterReferences LowerThrusters;
        public ThrusterReferences UpperThrusters;

        private Rigidbody _rigidbody;

        private ThrusterState _lowerThrusterState, _upperThrusterState;
        private float _engineState;
        
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            _engineState = Input.GetMouseButton(0) ? 1f : 0f;
            _lowerThrusterState = new ThrusterState()
            {
                Left = Input.GetKey(KeyCode.F) ? 1f : 0f,
                Right = Input.GetKey(KeyCode.H) ? 1f : 0f,
                Front = Input.GetKey(KeyCode.T) ? 1f : 0f,
                Back = Input.GetKey(KeyCode.G) ? 1f : 0f,
            };
            _upperThrusterState = new ThrusterState()
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

            _rigidbody.AddForceAtPosition(LowerThrusters.Left.up * _lowerThrusterState.Left * ThrusterForce, LowerThrusters.Left.position);
            _rigidbody.AddForceAtPosition(LowerThrusters.Right.up * _lowerThrusterState.Right * ThrusterForce, LowerThrusters.Right.position);
            _rigidbody.AddForceAtPosition(LowerThrusters.Front.up * _lowerThrusterState.Front * ThrusterForce, LowerThrusters.Front.position);
            _rigidbody.AddForceAtPosition(LowerThrusters.Back.up * _lowerThrusterState.Back * ThrusterForce, LowerThrusters.Back.position);
            
            _rigidbody.AddForceAtPosition(UpperThrusters.Left.up * _upperThrusterState.Left * ThrusterForce, UpperThrusters.Left.position);
            _rigidbody.AddForceAtPosition(UpperThrusters.Right.up * _upperThrusterState.Right * ThrusterForce, UpperThrusters.Right.position);
            _rigidbody.AddForceAtPosition(UpperThrusters.Front.up * _upperThrusterState.Front * ThrusterForce, UpperThrusters.Front.position);
            _rigidbody.AddForceAtPosition(UpperThrusters.Back.up * _upperThrusterState.Back * ThrusterForce, UpperThrusters.Back.position);
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