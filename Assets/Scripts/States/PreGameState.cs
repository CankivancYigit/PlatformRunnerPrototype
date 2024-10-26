using UnityEngine;

public class PreGameState : State
{
    [SerializeField] private StateMachine stateMachine;
    [SerializeField] private State runningState;
    
    public override void EnterState()
    {
        if (!enabled)
        {
            enabled = true;
        }
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
