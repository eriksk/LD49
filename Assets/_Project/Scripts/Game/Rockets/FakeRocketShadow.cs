using UnityEngine;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets
{
    public class FakeRocketShadow : MonoBehaviour
    {
        private RocketController _rocket;

        void Start()
        {
            _rocket = GetComponentInParent<RocketController>();
        }

        void Update()
        {
            var parentTransform = _rocket.transform;

            var altitude = _rocket.Altitude;

            transform.position = new Vector3(
                parentTransform.position.x,
                0.1f,
                parentTransform.position.z
            );
            transform.localScale = Vector3.one * Mathf.Clamp(altitude * 0.15f, 0f, 30f);
            if (altitude < 10)
            {
                transform.localScale = Vector3.zero;
            }
            transform.rotation = Quaternion.identity;
        }
    }
}