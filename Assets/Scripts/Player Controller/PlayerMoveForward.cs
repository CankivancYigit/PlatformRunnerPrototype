using UnityEngine;

public class PlayerMoveForward : SingletonBase<PlayerMoveForward>
{
    public float moveSpeed = 5.0f;
    private float _currentSpeed;

    private void Start()
    {
        _currentSpeed = 0;
    }
    
    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(OnLevelStart);
        EventBus<PlayerCollidedEvent>.AddListener(OnPlayerCollide);
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
        EventBus<PlayerCollidedEvent>.RemoveListener(OnPlayerCollide);
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
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
        transform.Translate(Vector3.forward * (_currentSpeed * Time.deltaTime));
    }
}
