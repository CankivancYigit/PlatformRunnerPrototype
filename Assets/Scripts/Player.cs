using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _startPos;
    private Rigidbody _rigidbody;
    private Collider _collider;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _startPos = transform.position;
    }
    
    private void OnEnable()
    {
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachedFinish);
        EventBus<PlayerCollidedEvent>.AddListener(OnPlayerCollide);
        EventBus<PlayerKnockBackHappenedEvent>.AddListener(OnKnockBackHappened);
    }
    
    private void OnDisable()
    {
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachedFinish);
        EventBus<PlayerCollidedEvent>.RemoveListener(OnPlayerCollide);
        EventBus<PlayerKnockBackHappenedEvent>.RemoveListener(OnKnockBackHappened);
    }

    private void OnPlayerCollide(object sender, PlayerCollidedEvent @event)
    {
        _collider.enabled = false;
    }

    private void OnKnockBackHappened(object sender, PlayerKnockBackHappenedEvent @event)
    {
        ResetPositionToStart();
        _collider.enabled = true;
    }
    
    private void OnPlayerReachedFinish(object sender, PlayerReachedFinishEvent @event)
    {
        MovePlayerToWallPaintingPosition(@event);
    }

    private void ResetPositionToStart()
    {
        transform.position = _startPos;
        EventBus<PlayerPositionResetEvent>.Emit(this,new PlayerPositionResetEvent());
    }
    
    private void MovePlayerToWallPaintingPosition( PlayerReachedFinishEvent @event)
    {
        float speed = PlayerMoveForward.Instance.moveSpeed;
        Transform targetTransform = @event.WallPaintTransform;
        
        float duration = Vector3.Distance(transform.position, targetTransform.position) / speed;

        transform.DOMove(targetTransform.position, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            EventBus<PlayerReachedWallPaintingPosEvent>.Emit(this,new PlayerReachedWallPaintingPosEvent());
        });
    }


    public void ApplyHorizontalForce(float pushForce)
    {
        Vector3 pushDirection = transform.right * pushForce;
        _rigidbody.AddForce(pushDirection * Time.fixedTime, ForceMode.Acceleration);
    }
}
