using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour , ICollectible
{
    [SerializeField] private int coinValue = 5;
    [SerializeField] private GameObject coinSpritePrefab;
    private Canvas _canvas;
    private Transform _canvasTransform;

    private void Start()
    {
        _canvas = UIManager.Instance.canvas;
    }

    public void OnCollected()
    {
        CollectCoin();
    }
    
    private void CollectCoin()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        StartCoroutine(ShowCoinSpritesAndSendThemToCounter(screenPosition));
        gameObject.GetComponent<MeshRenderer>().enabled = false; //Coroutine tamamen calisabilsin diye once objenin renderer ini kapatiyoruz
        Destroy(gameObject,2);//Objeyi belirli bir saniye sonra destroy ediyoruz 
    }

    private IEnumerator ShowCoinSpritesAndSendThemToCounter(Vector3 position)
    {
        for (int i = 0; i < coinValue; i++)
        {
            GameObject coinSprite = Instantiate(coinSpritePrefab, _canvas.transform);
            RectTransform rectTransform = coinSprite.GetComponent<RectTransform>();
            rectTransform.position = position;
            SendCoin(coinSprite);
            yield return new WaitForSeconds(0.1f);
        }
    }
    
    private void SendCoin(GameObject coin)
    {
        coin.transform.DOMove(CoinCounter.Instance.coinImageTransform.position, 1).OnComplete(() =>
        {
            EventBus<CoinCollectedEvent>.Emit(this,new CoinCollectedEvent());
            Destroy(coin);
        });
    }
}
