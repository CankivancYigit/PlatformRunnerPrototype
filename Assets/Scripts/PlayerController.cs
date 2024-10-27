using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMoveSpeed = 10f; 
    public float horizontalLeftLimit = -3f;  
    public float horizontalRightLimit = 3f;
    
    private bool isDragging = false;  
    private Vector3 lastPlayerPosition;   
    private float lastMousePosX;
    
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
        lastPlayerPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true; 
            lastMousePosX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            
            float currentMousePosX = Input.mousePosition.x;
            float mouseDeltaX = currentMousePosX - lastMousePosX;

            // X eksenindeki hareketi hesaplayıp karakterin pozisyonuna uyguluyoruz
            float moveX = mouseDeltaX / Screen.width * (horizontalRightLimit - horizontalLeftLimit);
            Vector3 targetPosition = new Vector3(lastPlayerPosition.x + moveX, transform.position.y, transform.position.z);
            
            targetPosition.x = Mathf.Clamp(targetPosition.x, horizontalLeftLimit, horizontalRightLimit);
            
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * horizontalMoveSpeed);
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            lastPlayerPosition = transform.position;
        }
    }
}

