using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Path.Obstacles
{
    public class ObstacleUpDown : PathBase
    {
        [Range(.5f,2f)]
        public float Speed = 1f;

        public Transform[] Uppers;

        private void Start()
        {
            for (int i = 0; i < Uppers.Length; i++)
                MoveUp(Uppers[i], i);
        }

        private void MoveUp(Transform upper, int i)
        {
            upper.DOLocalMoveY(1.5f, .5f / Speed).SetEase(Ease.OutBounce).SetDelay(i / Speed + 1 / Speed).OnComplete(() => MoveDown(upper));
        }

        private void MoveDown(Transform upper)
        {
            upper.DOLocalMoveY(0, 1 / Speed).SetEase(Ease.InQuart).OnComplete(() => MoveUp(upper, 0)).SetDelay(.3f / Speed);
        }

        #region IPoolable
        public override void Setup()
        {
            Pool = PoolTag.UpDown;
        }
        public override void ReturnToPool()
        {
            //for (int i = 0; i < Uppers.Length; i++)
            //    Uppers[i].DOKill();

            base.ReturnToPool();
        }
        #endregion
    }
}