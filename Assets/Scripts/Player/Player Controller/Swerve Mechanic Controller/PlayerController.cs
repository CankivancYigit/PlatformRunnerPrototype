using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMoveSpeed = 10f; 
    public float horizontalLeftLimit = -3f;  
    public float horizontalRightLimit = 3f;
    
    private bool _isDragging;  
    private Vector3 _lastPlayerPosition;   
    private float _lastMousePosX;
    
    private void OnEnable()
    {
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachedFinish);
    }

    private void OnDisable()
    {
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachedFinish);
    }

    private void OnPlayerReachedFinish(object sender, PlayerReachedFinishEvent @event)
    {
        enabled = false;
    }

    void Start()
    {
        _lastPlayerPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true; 
            _lastMousePosX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0) && _isDragging)
        {
            
            float currentMousePosX = Input.mousePosition.x;
            float mouseDeltaX = currentMousePosX - _lastMousePosX;

            // X eksenindeki hareketi hesaplayıp karakterin pozisyonuna uyguluyoruz
            float moveX = mouseDeltaX / Screen.width * (horizontalRightLimit - horizontalLeftLimit);
            Vector3 targetPosition = new Vector3(_lastPlayerPosition.x + moveX, transform.position.y, transform.position.z);
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, horizontalLeftLimit, horizontalRightLimit);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * horizontalMoveSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
            _lastPlayerPosition = transform.position;
        }
    }
}

