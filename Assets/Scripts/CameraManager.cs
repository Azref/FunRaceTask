using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform Target;

    [Range(0.1f, 1f)]
    public float SmoothFactor = .5f;

    public float LookFrontDistance = 2f;

    private Vector3 _followDistance;

    private Vector3 _velocity;

    void Start()
    {
        _followDistance = transform.position - Target.position;
    }

    void Update()
    {
        Move();

        Look();
    }

    private void Look()
    {
        transform.LookAt(Target.position + Target.forward * LookFrontDistance);
    }

    public void Move(bool soft = true)
    {
        transform.position = Vector3.SmoothDamp(transform.position, Target.position + _followDistance, ref _velocity, SmoothFactor);
    }
}
