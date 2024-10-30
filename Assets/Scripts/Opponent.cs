using System;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
	public float runningSpeed = 6f;
	public Transform finishLine;
	public float respawnDelay = 2f;
	private Vector3 startPosition;
	private NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.speed = 0;
		startPosition = transform.position;
        
		if (finishLine != null)
		{
			agent.SetDestination(finishLine.position);
		}
	}

	private void OnEnable()
	{
		EventBus<LevelStartEvent>.AddListener(OnLevelStart);
	}

	private void OnDisable()
	{
		EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
	}

	private void OnLevelStart(object sender, LevelStartEvent @event)
	{
		agent.speed = runningSpeed;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<ICollideable>(out var collideable))
		{
			Invoke(nameof(Respawn),0);
		}
	}

	void Respawn()
	{
		transform.position = startPosition;
		agent.SetDestination(finishLine.position);
	}
}