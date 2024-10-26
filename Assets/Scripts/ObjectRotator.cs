using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    public Vector3 rotationAxes = Vector3.zero;
    public float rotationSpeed = 10f;

    void Update()
    {
        transform.Rotate(rotationAxes * (rotationSpeed * Time.deltaTime));
    }
}

