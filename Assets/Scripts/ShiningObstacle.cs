using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class ShiningObstacle : MonoBehaviour , ICollideable
{
    public float rotationSpeed = 50f;
    public float horizontalMoveSpeed = 2f;
    public float horizontalMoveRange = 5f;
    private float _initialX;
    private Vector3 _objectScale;

    private Tween _punchTween;
    
    public ParticleSystem obstacleParticle;

    void Start()
    {
        _initialX = transform.position.x;
        _objectScale = transform.localScale;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * (rotationSpeed * Time.deltaTime));

        float newX = Mathf.PingPong(Time.time * horizontalMoveSpeed, horizontalMoveRange) + (_initialX - horizontalMoveRange / 2);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }
    
    public void OnCollide()
    {
        ChangeParticleColor(Random.ColorHSV());
        PunchAnim();
    }
    
    void ChangeParticleColor(Color newColor)
    {
        var main = obstacleParticle.main;
        main.startColor = newColor;
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
