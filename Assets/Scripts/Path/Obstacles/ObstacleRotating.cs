using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Path.Obstacles
{
    public class ObstacleRotating : PathBase
    {
        public bool IsClockwise;

        [Range(.5f,2f)]
        public float Speed = 1f;

        public Transform Rotator;

        void Update()
        {
            Rotator.eulerAngles += (IsClockwise ? .4f:-.4f) * Vector3.up / Speed;
        }

        #region IPoolable
        public override void Setup()
        {
            Pool = PoolTag.Rotating;
        }
        #endregion
    }
}