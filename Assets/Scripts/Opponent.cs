using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Opponent : MonoBehaviour
{
	public float runningSpeed = 6f;
	public Transform finishLine;
	public float respawnDelay = 2f;
	private Vector3 _startPosition;
	private NavMeshAgent _agent;
	private bool _isReachedFinish;
	void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_agent.speed = 0;
		_startPosition = transform.position;
        
		if (finishLine != null)
		{
			_agent.SetDestination(finishLine.position);
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

	private void Update()
	{
		if (_agent.remainingDistance <= _agent.stoppingDistance && !_isReachedFinish)
		{
			EventBus<OpponentReachedFinishEvent>.Emit(this,new OpponentReachedFinishEvent(this));
			_isReachedFinish = true;
		}
	}

	private void OnLevelStart(object sender, LevelStartEvent @event)
	{
		_agent.speed = runningSpeed;
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
		transform.position = _startPosition;
		_agent.SetDestination(finishLine.position);
	}
}