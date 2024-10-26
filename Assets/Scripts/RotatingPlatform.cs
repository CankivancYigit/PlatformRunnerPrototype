using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool isRotatingTowardsRight;
    void Update()
    {
        if (isRotatingTowardsRight)
        {
            transform.Rotate(new Vector3(0,0,-1) * (rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0,0,1) * (rotationSpeed * Time.deltaTime));
        }
        
    }
    
    
}
