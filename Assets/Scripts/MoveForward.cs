using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = 0;
    }
    
    void Update()
    {
        // Move the character forward in its current direction
        transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
    }
}
