using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] float rotateSpeed = .25f;

    void Update()
    {
        RotateOnYAxis();
    }

    void RotateOnYAxis()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
}
