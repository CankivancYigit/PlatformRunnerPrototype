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
}
