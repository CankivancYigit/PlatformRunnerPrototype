using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private void OnEnable()
	{
		EventBus<LevelStartEvent>.AddListener(TransitionToRunning);
		EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
	}

	private void OnDisable()
	{
		EventBus<LevelStartEvent>.RemoveListener(TransitionToRunning);
		EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
	}
	
	private void OnPlayerReachedWallPaintingPos(object sender, PlayerReachedWallPaintingPosEvent @event)
	{
		SetBoolParameter("isRunning",false);
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
