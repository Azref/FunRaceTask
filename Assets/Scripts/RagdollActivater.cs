using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivater : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
        Debug.Log("++ OnTriggerEnter");

        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<Animator>().enabled = false;
        }
    }

    private void Reset()
    {
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<Animator>().enabled = true;
    }
}
