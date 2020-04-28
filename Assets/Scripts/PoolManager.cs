using Assets.Scripts.Enums;
using Assets.Scripts.Path;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class Pool
    {
        public PoolTag Tag;

        public GameObject Prefab;

        public int Size = 1;
    }

    public class PoolManager : Singleton<PoolManager>
    {
        [Title("Pools")]
        [InfoBox("Each pool will be limited with the amount of 'Size'. If you want to add more prefab to each pool when it goes empty, make toggle button ON", InfoMessageType.Info)]
        [InfoBox("If you want to limit the pool with 'Size', keep that 'ScalablePool' toggle button off.", InfoMessageType.Warning)]
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 20)]
        public bool ScalablePool;

        public List<Pool> Pools;

        private List<GameObject> _objectPool;

        public Dictionary<PoolTag, List<GameObject>> PoolDictionary;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            PoolDictionary = new Dictionary<PoolTag, List<GameObject>>();

            GeneratePools();
        }

        private void GeneratePools()
        {
            foreach (var pool in Pools)
            {
                if (PoolDictionary.ContainsKey(pool.Tag))
                    _objectPool = PoolDictionary[pool.Tag];
                else
                {
                    _objectPool = new List<GameObject>();
                    PoolDictionary.Add(pool.Tag, _objectPool);
                }

                for (int a = 0; a < pool.Size; a++)
                {
                    GameObject go = Instantiate(pool.Prefab, transform);
                    go.GetComponent<IPoolable>().Setup();
                    go.SetActive(false);
                    _objectPool.Add(go);
                }
            }
        }
    }

    public static class PoolExtension
    {
        public static PathBase GetPath(PoolTag tag, Transform prnt = null)
        {
            PathBase path = null;

            return path.GetPath(tag, prnt);
        }

        public static PathBase GetPath(this PathBase path, PoolTag tag, Transform prnt = null)
        {
            GameObject go = PoolManager.Instance.PoolDictionary[tag][0];

            path = go.GetComponent<PathBase>();

            PoolManager.Instance.PoolDictionary[tag].Remove(go);

            go.transform.SetParent(prnt);

            go.GetComponent<IPoolable>().GetFromPool();

            return path;
        }

        public static PathBase GetRandomPath(this PathBase path, Transform prnt = null)
        {
            var pool = PoolManager.Instance.transform;

            GameObject go = pool.GetChild(UnityEngine.Random.Range(0, pool.childCount)).gameObject;

            path = go.GetComponent<PathBase>();

            PoolManager.Instance.PoolDictionary[path.Pool].Remove(go);

            go.transform.SetParent(prnt);

            go.GetComponent<IPoolable>().GetFromPool();

            return path;
        }

        public static PathBase Kill(this PathBase path)
        {
            var poolable = path.GetComponent<IPoolable>();

            poolable.ReturnToPool();

            path.transform.SetParent(PoolManager.Instance.transform);

            PoolManager.Instance.PoolDictionary[poolable.Pool].Add(path.gameObject);

            return path;
        }
    }
}