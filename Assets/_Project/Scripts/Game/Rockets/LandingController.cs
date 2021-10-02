using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets
{
    public class LandingController
    {
        private float _timeStandingStill;
        private bool _isLanding;
        private List<float> _contacts;
        private bool _crashed;

        public bool IsLanding => _isLanding;
        public bool Landed => _timeStandingStill > 3f && !_crashed;
        public bool Done { get; private set; }
        public float LandingPrecision { get; set; }

        public event Action OnLandedSuccessfully;
        public event Action OnCrash;

        public LandingController()
        {
            _contacts = new List<float>(32);
            Done = false;

            OnLandedSuccessfully += () => Debug.Log($"LANDING SUCCESSFUL! Precision: {LandingPrecision}");
            OnCrash += () => Debug.Log("CRASH SUCCESSFUL!");
        }

        public void Update(RocketController rocket)
        {
            if (Done) return;

            if (Landed)
            {
                LandingPrecision = Vector3.Distance(rocket.transform.position, Vector3.zero);
                Done = true;
                OnLandedSuccessfully?.Invoke();
            }
        }

        public void RegisterCollision(RocketController rocket, Collision collision)
        {
            if (Done) return;

            var contactMagnitude = collision.relativeVelocity.magnitude;
            Debug.Log("CONTACT: " + contactMagnitude);
            _contacts.Add(contactMagnitude);

            if (contactMagnitude > 10f)
            {
                Debug.Log("CRASH!");
                _crashed = true;
                Done = true;
                OnCrash?.Invoke();
            }
        }

        public void RegisterInSafeZone(RocketController rocket)
        {
            if (Done) return;

            if(rocket.Rigidbody == null) return;

            var speed = rocket.Rigidbody.velocity.magnitude;
            var angularSpeed = rocket.Rigidbody.angularVelocity.magnitude;

            if (speed < 1f && angularSpeed < 1f)
            {
                _timeStandingStill += Time.deltaTime;
                _isLanding = true;
            }
            else
            {
                ClearLandingCheck();
            }
        }

        private void ClearLandingCheck()
        {
            _timeStandingStill = 0f;
            _isLanding = false;
        }
    }
}