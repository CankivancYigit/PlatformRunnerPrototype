using UnityEngine;

public class PlayerMoveForward : SingletonBase<PlayerMoveForward>
{
    public float moveSpeed = 5.0f;
    private float _currentSpeed;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _currentSpeed = 0;
    }
    
    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(OnLevelStart);
        EventBus<PlayerCollidedEvent>.AddListener(OnPlayerCollide);
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerPositionResetEvent>.AddListener(OnPlayerPositionReset);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
        EventBus<PlayerCollidedEvent>.RemoveListener(OnPlayerCollide);
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerPositionResetEvent>.RemoveListener(OnPlayerPositionReset);
    }

    private void OnPlayerPositionReset(object sender, PlayerPositionResetEvent @event)
    {
        _currentSpeed = moveSpeed;
    }

    private void OnPlayerReachedWallPaintingPos(object sender, PlayerReachedWallPaintingPosEvent @event)
    {
        _currentSpeed = 0;
    }
    
    private void OnPlayerCollide(object sender, PlayerCollidedEvent @event)
    {
        _currentSpeed = 0;
    }

    private void OnLevelStart(object sender, LevelStartEvent @event)
    {
        _currentSpeed = moveSpeed;
    }
    
    void Update()
    {
        _rigidbody.velocity = Vector3.forward * _currentSpeed;
    }
}
