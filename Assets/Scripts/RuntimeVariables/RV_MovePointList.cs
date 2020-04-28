using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Path;

[CreateAssetMenu(menuName = "Runtime Variables/Move Point List", order = 1)]
public class RV_MovePointList : SerializedScriptableObject
{
    public List<MovePointVo> list = new List<MovePointVo>();
}
