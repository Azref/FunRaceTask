using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
            GetComponent<Animator>().SetBool("IsRunning", true);
        else
            GetComponent<Animator>().SetBool("IsRunning", false);
    }
}
