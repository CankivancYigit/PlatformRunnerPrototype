using UnityEngine;

public class JoystickPlayerController : MonoBehaviour
{
    public float horizontalSpeed = 5f;
    public Joystick joystick;

    void Update()
    {
        float horizontalInput = joystick.Horizontal;
        
        transform.Translate(new Vector3(horizontalInput * horizontalSpeed * Time.deltaTime, 0, 0));
    }
}


