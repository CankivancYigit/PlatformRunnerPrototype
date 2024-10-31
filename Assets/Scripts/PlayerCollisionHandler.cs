using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollectible collectible))
        {
            collectible.OnCollected();
        }

        if (other.TryGetComponent(out ICollideable collideable))
        {
            collideable.OnCollide();
            KnockBack();
            EventBus<PlayerCollidedEvent>.Emit(this,new PlayerCollidedEvent());
        }
        
        if (other.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteraction();
        }
    }
    
    private void KnockBack()
    {
        transform.DOMoveZ(transform.position.z - 4, .5f).SetEase(Ease.OutQuad).OnComplete(delegate
        {
            EventBus<PlayerKnockBackHappenedEvent>.Emit(this,new PlayerKnockBackHappenedEvent());
        });
    }
}
