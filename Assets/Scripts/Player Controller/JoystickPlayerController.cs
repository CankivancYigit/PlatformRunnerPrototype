using System;
using UnityEngine;

public class JoystickPlayerController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public Joystick joystick;
    public float maxTiltAngle = 40f;
    public bool enableTilting = true;
    private Rigidbody _rigidbody;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachedFinish);
    }

    private void OnDisable()
    {
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachedFinish);
    }

    private void OnPlayerReachedFinish(object sender, PlayerReachedFinishEvent @event)
    {
        enabled = false;
    }

    void FixedUpdate()
    {
        float horizontalInput = joystick.Horizontal;
    
        _rigidbody.velocity = new Vector3(horizontalInput * horizontalSpeed, _rigidbody.velocity.y, _rigidbody.velocity.z);
    
        Vector3 clampedPosition = _rigidbody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        _rigidbody.position = clampedPosition;

        if (enableTilting)
        {
            float tiltAngle = horizontalInput * maxTiltAngle;
            Quaternion targetRotation = Quaternion.Euler(0f, tiltAngle, 0f);
            _rigidbody.MoveRotation(Quaternion.Lerp(_rigidbody.rotation, targetRotation, Time.deltaTime * 5f));
        }
    }
}



