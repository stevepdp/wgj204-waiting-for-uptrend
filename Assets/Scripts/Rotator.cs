using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private float rotateSpeed = 0.25f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
