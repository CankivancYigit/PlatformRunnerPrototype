using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public bool isRotatingTowardsRight;
    public float pushForce = 1f;
    public float minXPosition = -4f;
    public float maxXPosition = 4f;

    void Update()
    {
        if (isRotatingTowardsRight)
        {
            transform.Rotate(new Vector3(0,0,-1) * (rotationSpeed * Time.deltaTime));
        }
        else
        {
            transform.Rotate(new Vector3(0,0,1) * (rotationSpeed * Time.deltaTime));
        }
        
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            // Platformun dönüş yönünü belirle
            float direction = isRotatingTowardsRight ? 1f : -1f;

            // Player'a dönme yönünde yatay kuvvet uygula
            player.ApplyHorizontalForce(direction * pushForce);
            ClampPlayerPosition(player);
        }
        
        // Eğer çarpışan obje Opponent bileşenine sahipse
        if (other.TryGetComponent(out Opponent opponent))
        {
            float direction = isRotatingTowardsRight ? 1f : -1f;

            // Opponent'e dönme yönünde yatay kuvvet uygula
            opponent.ApplyHorizontalForce(direction * pushForce);
            ClampOpponentPosition(opponent);
        }
    }
    
    private void ClampOpponentPosition(Opponent opponent)
    {
        // Opponent'in pozisyonunu clamp'le
        Vector3 clampedPosition = opponent.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minXPosition, maxXPosition);
        opponent.transform.position = clampedPosition;
    }
    
    private void ClampPlayerPosition(Player player)
    {
        // Player'ın pozisyonunu clamp'le
        Vector3 clampedPosition = player.transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minXPosition, maxXPosition);
        player.transform.position = clampedPosition;
    }
}
