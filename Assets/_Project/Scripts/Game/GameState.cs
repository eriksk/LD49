using UnityEngine;
using Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Rockets;
using Assembly_CSharp_Editor.Assets._Project.Scripts.Game.Cameras;
using Assembly_CSharp_Editor.Assets._Project.Scripts.Game.UI;
using System.Collections;

namespace Assembly_CSharp_Editor.Assets._Project.Scripts.Game
{
    public class GameState : MonoBehaviour
    {
        public GameObject RocketPrefab;
        public CameraController Camera;
        public RocketUI UI;
        public float StartingHeight = 500f;
        public float StartingSpeedInKmh = 100f;

        private RocketController _rocket;
        private PlayState _state;

        void Start()
        {
            ClearState();
            StartCoroutine(StartSlowly());
        }

        private void ClearState()
        {
            _state = PlayState.PreGame;
            var existingRocket = GameObject.FindObjectOfType<RocketController>();
            if (existingRocket != null)
            {
                Destroy(existingRocket.gameObject);
            }
            Camera.Target = null;
            UI.Rocket = null;
        }

        void Update()
        {
            if (_state == PlayState.Playing && Input.GetKey(KeyCode.Return))
            {
                ClearState();
                StartCoroutine(StartSlowly());
            }
        }

        private IEnumerator StartSlowly()
        {
            yield return new WaitForSeconds(3);
            _rocket = Instantiate(RocketPrefab).GetComponent<RocketController>();
            var startingRotation = Quaternion.Euler(
                UnityEngine.Random.Range(-15, 15),
                0f,
                180f + UnityEngine.Random.Range(-15, 15)
            );
            _rocket.transform.position = Vector3.up * StartingHeight;
            _rocket.transform.rotation = startingRotation;
            _rocket.GetComponent<Rigidbody>().velocity = Vector3.down * (StartingSpeedInKmh / 3.6f);
            Camera.Target = _rocket.transform;
            UI.Rocket = _rocket;
            _state = PlayState.Playing;
        }
    }

    public enum PlayState
    {
        PreGame,
        Playing
    }
}