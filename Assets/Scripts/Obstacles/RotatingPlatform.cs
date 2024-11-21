using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool isRotatingTowardsRight;
    public float pushForce = 1f;
    public float minXPosition = -4f;
    public float maxXPosition = 4f;

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
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            float direction = isRotatingTowardsRight ? 1f : -1f;
            
            player.ApplyHorizontalForce(direction * pushForce * 30);//Hardcoded çarpım nedeni player ve opponent'a uygulanacak force'u manuel balanslamak
            ClampPosition(player.transform);
        }
        
        if (other.TryGetComponent(out Opponent opponent))
        {
            float direction = isRotatingTowardsRight ? 1f : -1f;
            
            opponent.ApplyHorizontalForce(direction * pushForce);
            ClampPosition(opponent.transform);
        }
    }
    
    private void ClampPosition(Transform target)
    {
        Vector3 clampedPosition = target.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minXPosition, maxXPosition);
        target.position = clampedPosition;
    }
}
