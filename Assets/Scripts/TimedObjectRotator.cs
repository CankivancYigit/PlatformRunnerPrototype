using UnityEngine;

public class TimedObjectRotator : MonoBehaviour
{
    public Vector3 rotationAxes = Vector3.zero;
    public float rotationSpeed = 10f;
    public float rotationDuration = 2f;
    public float pauseDuration = 2f;

    private float timer = 0f;
    private bool isRotating = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (isRotating)
        {
            transform.Rotate(rotationAxes * (rotationSpeed * Time.deltaTime));

            if (timer >= rotationDuration)
            {
                isRotating = false;
                timer = 0f;
            }
        }
        else
        {
            if (timer >= pauseDuration)
            {
                isRotating = true;
                timer = 0f;
            }
        }
    }
}


