using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Opponent : MonoBehaviour
{
	public float runningSpeed = 6f;
	public GameObject nameDisplayer;
	
	private Transform _finishLineTransform;
	private Vector3 _startPosition;
	private float _targetZPosition;
	private NavMeshAgent _agent;
	private Collider _collider;
	private Rigidbody _rigidbody;
	private bool _isReachedFinish;
	void Start()
	{
		_agent = GetComponent<NavMeshAgent>();
		_rigidbody = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
		
		nameDisplayer.SetActive(false);
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
		EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachFinish);
	}

	private void OnDisable()
	{
		EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
		EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachFinish);
	}

	private void OnPlayerReachFinish(object sender, PlayerReachedFinishEvent @event)
	{
		_agent.enabled = false;
		nameDisplayer.SetActive(false);
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
		nameDisplayer.SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out ICollideable collideable))
		{
			Respawn();
			collideable.OnCollide();
		}
	}

	void Respawn()
	{
		transform.position = _startPosition;
		if (_agent.isActiveAndEnabled)
		{
			_agent.SetDestination(_finishLineTransform.position);
		}
	}

	public void ApplyHorizontalForce(float pushForce)
	{
		Vector3 pushDirection = transform.right * pushForce;
		_rigidbody.AddForce(pushDirection);
	}
}