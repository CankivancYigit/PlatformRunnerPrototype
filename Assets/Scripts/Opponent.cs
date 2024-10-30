using System;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
	public Transform finishLine;
	public float respawnDelay = 2f;
	private Vector3 startPosition;
	private NavMeshAgent agent;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		startPosition = transform.position;
        
		if (finishLine != null)
		{
			agent.SetDestination(finishLine.position);
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<ICollideable>(out var collideable))
		{
			Invoke(nameof(Respawn), respawnDelay);
		}
	}

	void Respawn()
	{
		transform.position = startPosition;
		agent.SetDestination(finishLine.position);
	}
}