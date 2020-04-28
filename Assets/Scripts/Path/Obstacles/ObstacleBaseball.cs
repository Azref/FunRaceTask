using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Path.Obstacles
{
    public class ObstacleBaseball : PathBase
    {
        [Range(.5f,2f)]
        public float Speed = 1;

        public Transform Stick;

        private void Start()
        {
            Prepare();
        }

        private void Prepare()
        {
            Stick.DOLocalRotate(Vector3.up*110, 5f / Speed).SetEase(Ease.InOutQuad).SetDelay(1 / Speed).OnComplete(Hit);
        }

        private void Hit()
        {
            Stick.DOLocalRotate(Vector3.zero, 2f / Speed).SetEase(Ease.OutBounce).SetDelay(.2f / Speed).OnComplete(Prepare);
        }

        #region IPoolable
        public override void Setup()
        {
            Pool = PoolTag.Baseball;
        }
        #endregion
    }
}