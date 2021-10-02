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
        public float StartingHeight = 500;

        private RocketController _rocket;

        void Start()
        {
            var existingRocket = GameObject.FindObjectOfType<RocketController>();
            if (existingRocket != null)
            {
                Destroy(existingRocket.gameObject);
            }
            Camera.Target = null;
            UI.Rocket = null;

            StartCoroutine(StartSlowly());
        }

        private IEnumerator StartSlowly()
        {
            yield return new WaitForSeconds(3);
            _rocket = Instantiate(RocketPrefab).GetComponent<RocketController>();
            _rocket.transform.position = Vector3.up * StartingHeight;
            Camera.Target = _rocket.transform;
            UI.Rocket = _rocket;
        }
    }
}