using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    public float maxRotationAngle = 30f;
    public float resetSpeed = 2f;       
    public float delayBeforeReset = 1f;

    private float currentRotationY = 0f; 
    private bool isDragging;      
    private Coroutine resetRotationCoroutine;  

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
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            
            if (resetRotationCoroutine != null)
            {
                StopCoroutine(resetRotationCoroutine);
            }
        }

        if (Input.GetMouseButton(0) && isDragging)
        {
            float mouseDeltaX = Input.GetAxis("Mouse X");
            currentRotationY += mouseDeltaX * maxRotationAngle;
            currentRotationY = Mathf.Clamp(currentRotationY, -maxRotationAngle, maxRotationAngle);

            // Mouse'a basılıyken, rotasyonu sıfırlamak için current Y rotasyonunu max açıya yaklaştığını check ediyoruz
            if (Mathf.Abs(currentRotationY) - maxRotationAngle > -4)
            {
                resetRotationCoroutine = StartCoroutine(SmoothResetRotation(delayBeforeReset));
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            resetRotationCoroutine = StartCoroutine(SmoothResetRotation(0));
        }
        
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
    }

    private IEnumerator SmoothResetRotation(float delay)
    {
        yield return new WaitForSeconds(delay);

        float targetRotationY = 0f;
        while (Mathf.Abs(currentRotationY) > 0.01f)
        {
            currentRotationY = Mathf.Lerp(currentRotationY, targetRotationY, Time.deltaTime * resetSpeed);
            transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
            yield return null;
        }

        currentRotationY = targetRotationY;
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
    }
}
