using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private Animator animator;
	private static readonly int IsRunning = Animator.StringToHash("isRunning");

	public void TransitionToRunningAnimation()
	{
		animator.SetBool(IsRunning,true);
	}
	
	public void TransitionToIdleAnimation()
	{
		animator.SetBool(IsRunning,false);
	}
}
