using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts
{
    public class CameraManager : MonoBehaviour
    {
        public RV_GameStatus Game;

        public Transform Target;

        [Range(0.1f, 1f)]
        public float SmoothFactor = .5f;

        public Transform RefCamPos;

        private Vector3 _followDistance;

        private Vector3 _velocity;

        void Start()
        {
            _followDistance = transform.position - Target.position;
        }

        void Update()
        {
            Move();

            Look();
        }

        private void Look()
        {
            transform.LookAt(Target.position);
        }

        public void Move(bool soft = true)
        {
            transform.position = Vector3.SmoothDamp(transform.position, DesiredPos(), ref _velocity, SmoothFactor);
        }

        private Vector3 DesiredPos()
        {
            return Target.position + (Game.status == GameStatus.IsStopped ? 2f : 1) *
                (Target.forward * _followDistance.z + Target.right * _followDistance.x + Target.up * _followDistance.y);
        }
    }
}