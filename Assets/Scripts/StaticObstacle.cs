using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StaticObstacle : MonoBehaviour, ICollideable
{
    Tween punchTween;
    private Vector3 objectScale;

    private void Awake()
    {
        objectScale = transform.localScale;
    }
    
    
    public void OnCollide()
    {
        PunchAnim();
    }
    
    private void PunchAnim()
    {
        if (punchTween != null)
        {
            punchTween.Kill(true);
        }

        punchTween = transform.DOPunchScale(objectScale * 0.2f, 0.1f, 7, 0.4f);
    }
}
