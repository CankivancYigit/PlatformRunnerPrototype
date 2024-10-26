using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ICollectible>(out ICollectible collectible))
        {
            collectible.OnCollected();
        }

        if (other.TryGetComponent<ICollideable>(out ICollideable collideable))
        {
            collideable.OnCollide();
            EventBus<PlayerCollidedEvent>.Emit(this,new PlayerCollidedEvent());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out RotatingPlatform rotatingPlatform))
        {
            if (rotatingPlatform.isRotatingTowardsRight)
            {
                transform.Translate(Vector3.right * rotatingPlatform.rotationSpeed/10 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector3.left * rotatingPlatform.rotationSpeed/10 * Time.deltaTime);
            }
        }
    }
}
