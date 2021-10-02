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
        public FuelState Fuel;

        public float BoosterFuelConsumptionPerSecond = 0.1f;
        public float MainEngineFuelConsumptionPerSecond = 0.1f;

        private Rigidbody _rigidbody;

        private ThrusterState _thrusterState;
        private float _engineState;

        private LandingController _landingController;

        public float Altitude => transform.position.y;
        public float VerticalSpeed => _rigidbody.velocity.y;
        public Rigidbody Rigidbody => _rigidbody;
        public LandingController LandingController => _landingController;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _landingController = new LandingController();
            Fuel.Reset();
        }

        void Update()
        {
            if (!_landingController.Done)
            {
                _engineState = Input.GetMouseButton(0) && Fuel.Main > 0f ? 1f : 0f;
                _thrusterState = new ThrusterState()
                {
                    Left = Input.GetKey(KeyCode.A) && Fuel.Left > 0f ? 1f : 0f,
                    Right = Input.GetKey(KeyCode.D) && Fuel.Right > 0f ? 1f : 0f,
                    Front = Input.GetKey(KeyCode.W) && Fuel.Front > 0f ? 1f : 0f,
                    Back = Input.GetKey(KeyCode.S) && Fuel.Back > 0f ? 1f : 0f,
                };
            }

            Fuel.Main -= MainEngineFuelConsumptionPerSecond * _engineState * Time.deltaTime;
            Fuel.Left -= BoosterFuelConsumptionPerSecond * _thrusterState.Left * Time.deltaTime;
            Fuel.Right -= BoosterFuelConsumptionPerSecond * _thrusterState.Right * Time.deltaTime;
            Fuel.Front -= BoosterFuelConsumptionPerSecond * _thrusterState.Front * Time.deltaTime;
            Fuel.Back -= BoosterFuelConsumptionPerSecond * _thrusterState.Back * Time.deltaTime;

            Fuel.Main = Mathf.Clamp01(Fuel.Main);
            Fuel.Left = Mathf.Clamp01(Fuel.Left);
            Fuel.Right = Mathf.Clamp01(Fuel.Right);
            Fuel.Front = Mathf.Clamp01(Fuel.Front);
            Fuel.Back = Mathf.Clamp01(Fuel.Back);

            SetParticleSystemEmission(Thrusters.MainBooster, _engineState > 0f);
            SetParticleSystemEmission(Thrusters.LeftBooster, _thrusterState.Left > 0f);
            SetParticleSystemEmission(Thrusters.RightBooster, _thrusterState.Right > 0f);
            SetParticleSystemEmission(Thrusters.FrontBooster, _thrusterState.Front > 0f);
            SetParticleSystemEmission(Thrusters.BackBooster, _thrusterState.Back > 0f);

            _landingController.Update(this);
        }

        void SetParticleSystemEmission(ParticleSystem system, bool enabled)
        {
            var emission = system.emission;
            emission.enabled = enabled;
        }

        void FixedUpdate()
        {
            _rigidbody.AddForce(transform.up * _engineState * EngineForce);

            _rigidbody.AddForceAtPosition(-Thrusters.Left.up * _thrusterState.Left * ThrusterForce, Thrusters.Left.position);
            _rigidbody.AddForceAtPosition(-Thrusters.Right.up * _thrusterState.Right * ThrusterForce, Thrusters.Right.position);
            _rigidbody.AddForceAtPosition(-Thrusters.Front.up * _thrusterState.Front * ThrusterForce, Thrusters.Front.position);
            _rigidbody.AddForceAtPosition(-Thrusters.Back.up * _thrusterState.Back * ThrusterForce, Thrusters.Back.position);
        }

        void OnCollisionEnter(Collision collision)
        {
            _landingController.RegisterCollision(this, collision);
        }

        void OnTriggerStay(Collider collider)
        {
            if (collider.name == "SafeZone")
            {
                _landingController.RegisterInSafeZone(this);
            }
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
        public ParticleSystem MainBooster;
        public ParticleSystem LeftBooster;
        public ParticleSystem RightBooster;
        public ParticleSystem FrontBooster;
        public ParticleSystem BackBooster;
    }

    [System.Serializable]
    public class FuelState
    {
        public float Left, Right, Front, Back;
        public float Main;

        public void Reset()
        {
            Left = 1f;
            Right = 1f;
            Front = 1f;
            Back = 1f;
            Main = 1f;
        }
    }
}