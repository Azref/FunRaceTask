using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleRotater : MonoBehaviour
{
    public bool IsClockwise;

    [Range(.5f,2f)]
    public float Speed = 1;

    void Update()
    {
        transform.eulerAngles += (IsClockwise ? 1:-1) * Speed * Vector3.up;
    }
}
