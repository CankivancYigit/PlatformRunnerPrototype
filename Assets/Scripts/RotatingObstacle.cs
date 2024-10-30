using System;
using DG.Tweening;
using UnityEngine;

public class RotatingObstacle : MonoBehaviour,ICollideable
{
    public Vector3 rotationAxes = Vector3.zero;
    public float rotationSpeed = 10f;
    
    private Tween _punchTween;
    private Vector3 _objectScale;

    private void Start()
    {
        _objectScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(rotationAxes * (rotationSpeed * Time.deltaTime));
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

        _punchTween = transform.DOPunchScale(_objectScale * 0.3f, 0.1f, 7, 0.4f);
    }
}

