using Assets.Scripts.Enums;
using UnityEngine;

namespace Assets.Scripts.Path
{
    public class PathBase : MonoBehaviour, IPoolable
    {
        public Transform MountPoint;

        public Transform MovePointHolder;

        #region IPoolable
        public PoolTag Pool { get; set; }

        public PoolTag Type;

        public virtual void Setup()
        {
            Pool = Type;
        }

        public virtual void GetFromPool()
        {
            gameObject.SetActive(true);
        }

        public virtual void ReturnToPool()
        {
            gameObject.SetActive(false);

            var AllFX = GetComponentsInChildren<HitFX>();

            foreach (HitFX fx in AllFX)
                fx.ResetFX();
        }
        #endregion
    }
}