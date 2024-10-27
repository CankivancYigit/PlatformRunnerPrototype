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
    
    private void KnockBack()
    {
        transform.DOMoveZ(transform.position.z - 4, .5f).SetEase(Ease.OutQuad);
    }
}
