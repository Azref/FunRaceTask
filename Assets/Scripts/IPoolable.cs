using Assets.Scripts.Enums;

namespace Assets.Scripts
{
    public interface IPoolable
    {
        void Setup();

        void GetFromPool();

        void ReturnToPool();

        PoolTag Pool { get; set; }
    }
}