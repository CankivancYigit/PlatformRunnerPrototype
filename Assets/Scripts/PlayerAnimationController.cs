using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	private static readonly int IsRunning = Animator.StringToHash("isRunning");

	private void OnEnable()
	{
		EventBus<LevelStartEvent>.AddListener(TransitionToRunning);
	}

	private void OnDisable()
	{
		EventBus<LevelStartEvent>.RemoveListener(TransitionToRunning);
	}

	private void TransitionToRunning(object sender, LevelStartEvent e)
	{
		TransitionToRunningAnimation();
	}
	
	public void TransitionToRunningAnimation()
	{
		animator.SetBool(IsRunning,true);
	}
	
	public void TransitionToIdleAnimation()
	{
		animator.SetBool(IsRunning,false);
	}
}
