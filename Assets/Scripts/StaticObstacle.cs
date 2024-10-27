using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StaticObstacle : MonoBehaviour, ICollideable
{
    private Tween _punchTween;
    private Vector3 _objectScale;

    private void Awake()
    {
        _objectScale = transform.localScale;
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
