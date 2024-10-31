using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(TransitionToRunning);
        EventBus<OpponentReachedFinishEvent>.AddListener(OnFinishReach);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(TransitionToRunning);
        EventBus<OpponentReachedFinishEvent>.RemoveListener(OnFinishReach);
    }

    private void OnFinishReach(object sender, OpponentReachedFinishEvent @event)
    {
        if (GetComponent<Opponent>() == @event.Opponent) //Eğer Eventi yollayan ve dinleyen Opponent eşleşiyorsa
        {
            SetBoolParameter("isRunning",false);
        }
    }

    private void TransitionToRunning(object sender, LevelStartEvent e)
    {
        SetBoolParameter("isRunning",true);
    }
	
    private void SetBoolParameter(string boolName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(boolName, value);
        }
    }
}
