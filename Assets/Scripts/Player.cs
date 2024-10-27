using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private Vector3 _startPos;
    //
    // private void Start()
    // {
    //     _startPos = transform.position;
    // }
    //
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
        MovePlayerToWallPaintingPosition(@event);
    }

    private void MovePlayerToWallPaintingPosition( PlayerReachedFinishEvent @event)
    {
        float speed = PlayerMoveForward.Instance.moveSpeed;
        Transform targetTransform = @event._wallPaintTransform;
        
        float duration = Vector3.Distance(transform.position, targetTransform.position) / speed;

        transform.DOMove(targetTransform.position, duration).SetEase(Ease.Linear).OnComplete(() =>
        {
            EventBus<PlayerReachedWallPaintingPosEvent>.Emit(this,new PlayerReachedWallPaintingPosEvent());
        });
    }
    //
    // private void OnPlayerCollide(object sender, PlayerCollidedEvent @event)
    // {
    //     ResetPositionToStart();
    // }
    //
    // public void ResetPositionToStart()
    // {
    //     transform.position = _startPos;
    // }
}
