using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour , ICollectible
{
    [SerializeField] private int coinValue = 5;
    public void OnCollected()
    {
        
    }
}
