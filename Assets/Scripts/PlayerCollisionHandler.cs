using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

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
                _rigidbody.AddForce(Vector3.right * (rotatingPlatform.rotationSpeed / 10) * Time.fixedTime);
            }
            else
            {
                _rigidbody.AddForce(Vector3.left * (rotatingPlatform.rotationSpeed / 10) * Time.fixedTime);
            }


            ClampOnRotatingPlatform();
        }
    }

    private void ClampOnRotatingPlatform()
    {
        Vector3 clampedPosition = _rigidbody.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -4, 4);
        _rigidbody.position = clampedPosition;
    }

    private void KnockBack()
    {
        transform.DOMoveZ(transform.position.z - 4, .5f).SetEase(Ease.OutQuad).OnComplete(delegate
        {
            EventBus<PlayerKnockBackHappenedEvent>.Emit(this,new PlayerKnockBackHappenedEvent());
        });
    }
}
