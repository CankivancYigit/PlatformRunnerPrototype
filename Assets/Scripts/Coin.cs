using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
	[SerializeField] private int coinValue = 5;
	private Canvas _canvas;

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
			GameObject coinSprite = ObjectPool.Instance.GetObject(); // Havuzdan al
			RectTransform rectTransform = coinSprite.GetComponent<RectTransform>();
			rectTransform.position = position; // Ekran pozisyonunu ayarla
			SendCoin(coinSprite);
			yield return new WaitForSeconds(0.1f); // Her bir sprite için bekle
		}
	}

	private void SendCoin(GameObject coin)
	{
		coin.transform.DOMove(CoinCounter.Instance.coinImageTransform.position, 1).OnComplete(() =>
		{
			EventBus<CoinCollectedEvent>.Emit(this, new CoinCollectedEvent());
			ObjectPool.Instance.ReturnObject(coin); // Sprite'ı havuza geri gönder
		});
	}
}