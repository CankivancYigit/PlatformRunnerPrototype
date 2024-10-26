using UnityEngine;

public class MoveForward : MonoBehaviour
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
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
    }

    private void OnLevelStart(object sender, LevelStartEvent e)
    {
        _currentSpeed = moveSpeed;
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * (_currentSpeed * Time.deltaTime));
    }
}
