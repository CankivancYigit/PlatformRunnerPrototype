using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerState : State
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private State wallPaintingState;

    private void OnEnable()
    {
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
    }

    public override void EnterState()
    {
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
    }

    private void OnPlayerReachedWallPaintingPos(object sender, PlayerReachedWallPaintingPosEvent @event)
    {
        stateMachine.ChangeState(wallPaintingState);
    }

    public override void ExitState()
    {
        
    }
}
