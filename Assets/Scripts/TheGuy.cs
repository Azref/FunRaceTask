using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheGuy : MonoBehaviour
{
    public RV_PathData PathData;

    public float RotSpeed = .5f;

    private int _targetPathPoint = 0;

    private Vector3 _rotVelocity;

    private bool _isRunning;

    public bool IsRunning
    {
        get { return _isRunning; }
        set
        {
            _isRunning = value;
            GetComponent<Animator>().SetBool("IsRunning", _isRunning);
        }
    }

    /// <summary>
    /// setup for characer.
    /// </summary>
    private void Start()
    {
        transform.position = PathData.list[0];

        IsRunning = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("crashed!!!");

            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<Animator>().enabled = false;
        }
    }

    private void Update()
    {
        CheckInput();

        CheckPath();

        Rotate();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButton(0))
            IsRunning = true;
        else
            IsRunning = false;
    }

    private void CheckPath()
    {

        if (Vector3.Distance(PathData.list[_targetPathPoint] , transform.position) < RotSpeed + .5f)
            _targetPathPoint++;
    }

    private void Rotate()
    {
        //if (!_isRunning)
        //    return;

        if (transform.hasChanged)
        {
            Debug.Log("++");
            transform.LookAt(Vector3.SmoothDamp(transform.position + transform.forward, PathData.list[_targetPathPoint], ref _rotVelocity, RotSpeed));
            transform.hasChanged = false;
        }
    }

    private void Reset()
    {
        IsRunning = false;

        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
