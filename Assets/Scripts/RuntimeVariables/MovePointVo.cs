using System;
using UnityEngine;

namespace Assets.Scripts.Path
{
    [Serializable]
    public class MovePointVo
    {
        public Vector3 Point;

        public Vector3 PointNormal;

        public bool IsRespawn = false;

    }
}