using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runtime Variables/Path Data List", order = 1)]
public class RV_PathData : ScriptableObject
{
    public List<Vector3> list = new List<Vector3>();
}
