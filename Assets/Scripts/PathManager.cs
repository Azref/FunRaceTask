using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    public RV_PathData PathData;

    private void Start()
    {
        BuildPath();
    }

    [Button(ButtonSizes.Gigantic, ButtonStyle.CompactBox)]
    public void BuildPath()
    {
        PathData.list = new List<Vector3>();

        for (int i = 0; i < transform.childCount; i++)
            PathData.list.Add(transform.GetChild(i).position + Vector3.up);
    }

}
