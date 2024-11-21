using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public float minX = -5f;
    public float maxX = 5f;
    public float maxTiltAngle = 40f;
    public bool enableTilting = true;
    public Joystick joystick;

    private Player _player;

    void Start()
    {
        _player = GetComponent<Player>();
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
        if (joystick == null || _player == null) return;
        
        _player.HorizontalInput = joystick.Horizontal;
        
        var rigidbody = _player.playerRigidbody;
        if (rigidbody == null) return;

        rigidbody.velocity = new Vector3(_player.HorizontalInput * horizontalSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
        
        Vector3 clampedPosition = rigidbody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minX, maxX);
        rigidbody.position = clampedPosition;
        
        if (enableTilting)
        {
            float tiltAngle = _player.HorizontalInput * maxTiltAngle;
            Quaternion targetRotation = Quaternion.Euler(0f, tiltAngle, 0f);
            rigidbody.MoveRotation(Quaternion.Lerp(rigidbody.rotation, targetRotation, Time.deltaTime * 5f));
        }
    }
}



