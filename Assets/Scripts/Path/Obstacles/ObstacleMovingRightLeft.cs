using Assets.Scripts.Enums;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Path.Obstacles
{
    public class ObstacleMovingRightLeft : PathBase
    {
        [Range(.5f,2f)]
        public float Speed = 1f;

        public Transform Platform;

        private bool _isLeft;

        private void Start()
        {
            goLeft();
        }

        private void goLeft()
        {
            Platform.DOLocalMoveX(-4, 2.5f / Speed).SetEase(Ease.Linear).OnComplete(goRight);
        }

        private void goRight()
        {
            Platform.DOLocalMoveX(4, 2.5f / Speed).SetEase(Ease.Linear).OnComplete(goLeft);
        }

        #region IPoolable
        public override void Setup()
        {
            Pool = PoolTag.MovingRightLeft;
        }
        #endregion
    }
}