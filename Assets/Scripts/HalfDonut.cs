using UnityEngine;

public class HalfDonut : MonoBehaviour
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    public float timeInterval = 2f;

    private Vector3 _startPosition;
    private float _timeElapsed = 0f;
    private bool _isMovingToEnd = false;
    private bool _isMoving = false;

    void Start()
    {
        _startPosition = transform.position;
    }

    void Update()
    {
        _timeElapsed += Time.deltaTime;

        if (_timeElapsed >= timeInterval && !_isMoving)
        {
            _isMoving = true;
            _isMovingToEnd = !_isMovingToEnd;
            _timeElapsed = 0f;
        }

        if (_isMoving)
        {
            Move();
        }
    }

    void Move()
    {
        if (_isMovingToEnd)
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition + Vector3.right * moveDistance, moveSpeed * Time.deltaTime);
            if (transform.position == _startPosition + Vector3.right * moveDistance)
            {
                _isMoving = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _startPosition, moveSpeed * Time.deltaTime);
            if (transform.position == _startPosition)
            {
                _isMoving = false;
            }
        }
    }
}
