using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Path.Obstacles
{
    public class ObstacleSqueezing : PathBase
    {
        [Range(.5f,2f)]
        public float Speed = 1f;

        public Transform Left, Rght;

        private void Start()
        {
            Close();
        }

        private void Close()
        {
            Left.DOLocalMoveX(-2, .5f / Speed).SetEase(Ease.OutBounce).SetDelay(.2f / Speed).OnComplete(Open);
            Rght.DOLocalMoveX(2, .5f / Speed).SetEase(Ease.OutBounce).SetDelay(.2f / Speed);
        }

        private void Open()
        {
            Left.DOLocalMoveX(-4, 2.5f / Speed).SetEase(Ease.OutQuart).SetDelay(.4f / Speed).OnComplete(Close);
            Rght.DOLocalMoveX(4, 2.5f / Speed).SetEase(Ease.OutQuart).SetDelay(.4f / Speed);
        }

        #region IPoolable
        public override void Setup()
        {
            Pool = PoolTag.Squeezing;
        }
        #endregion
    }
}