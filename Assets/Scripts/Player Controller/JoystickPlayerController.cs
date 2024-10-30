using UnityEngine;

public class JoystickPlayerController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public Joystick joystick;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
        
        _rigidbody.velocity = new Vector3(horizontalInput * horizontalSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z);
        
        Vector3 clampedPosition = _rigidbody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        _rigidbody.position = clampedPosition;
    }
}



