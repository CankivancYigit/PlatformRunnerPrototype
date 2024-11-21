using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float HorizontalInput { get; set; }
    public Rigidbody playerRigidbody;
    public Collider playerCollider;
    
    private Vector3 _startPos;
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<Collider>();
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
        playerCollider.enabled = false;
    }

    private void OnKnockBackHappened(object sender, PlayerKnockBackHappenedEvent @event)
    {
        ResetPositionToStart();
        playerCollider.enabled = true;
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
        playerRigidbody.AddForce(pushDirection);
    }
}
