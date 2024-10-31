using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Opponent : MonoBehaviour
{
	public float runningSpeed = 6f;
	
	private Transform _finishLineTransform;
	private Vector3 _startPosition;
	private float _targetZPosition;
	private NavMeshAgent _agent;
	private Collider _collider;
	
	private bool _isReachedFinish;
	void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_collider = GetComponent<Collider>();
		_agent.speed = 0;
		_agent.stoppingDistance = 2;
		_finishLineTransform = FinishLine.Instance.opponentFinishTransform;
		_startPosition = transform.position;
		_targetZPosition = _finishLineTransform.position.z;
        
		if (_finishLineTransform != null)
		{
			Vector3 targetPosition = new Vector3(_agent.transform.position.x, _agent.transform.position.y, _targetZPosition);
			_agent.SetDestination(targetPosition);
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
		if (Mathf.Abs(_agent.transform.position.z - _targetZPosition) <= _agent.stoppingDistance && !_isReachedFinish)
		{
			EventBus<OpponentReachedFinishEvent>.Emit(this,new OpponentReachedFinishEvent(this));
			_collider.enabled = false;
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
			Respawn();
		}
	}

	void Respawn()
	{
		transform.position = _startPosition;
		_agent.SetDestination(_finishLineTransform.position);
	}
}