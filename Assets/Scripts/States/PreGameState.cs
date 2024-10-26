using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PreGameState : State
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private State runningState;
    
    public override void EnterState()
    {
        
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateMachine.ChangeState(runningState);
            EventBus<LevelStartEvent>.Emit(this,new LevelStartEvent());
        }
    }


    public override void ExitState()
    {
        
    }
}
