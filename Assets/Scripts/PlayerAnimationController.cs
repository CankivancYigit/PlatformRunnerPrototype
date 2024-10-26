using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private Animator animator;

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
