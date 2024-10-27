using DG.Tweening;
using UnityEngine;

public class HalfDonut : MonoBehaviour , ICollideable
{
    public float moveDistance = 5f;
    public float moveSpeed = 2f;
    public float timeInterval = 2f;

    private Vector3 _startPosition;
    private float _timeElapsed;
    private bool _isMovingToEnd;
    private bool _isMoving;
    private Tween _punchTween;
    private Vector3 _objectScale;
    
    void Start()
    {
        _startPosition = transform.position;
        _objectScale = transform.localScale;
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

    public void OnCollide()
    {
        PunchAnim();
    }
    
    private void PunchAnim()
    {
        if (_punchTween != null)
        {
            _punchTween.Kill(true);
        }

        _punchTween = transform.DOPunchScale(_objectScale * 0.2f, 0.1f, 7, 0.4f);
    }
}
