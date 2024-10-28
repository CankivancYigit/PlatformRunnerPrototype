using System;
using TMPro;
using UnityEngine;

public class CoinCounter : SingletonBase<CoinCounter>
{
   public Transform coinImageTransform;
   public TextMeshProUGUI coinCounterText;
   public int coinCounter;

   private void OnEnable()
   {
      coinCounterText.text = coinCounter.ToString();
      EventBus<CoinCollectedEvent>.AddListener(OnCoinCollected);
   }

   private void OnDisable()
   {
      EventBus<CoinCollectedEvent>.RemoveListener(OnCoinCollected);
   }

   private void OnCoinCollected(object sender, CoinCollectedEvent @event)
   {
      coinCounter++;
      coinCounterText.text = coinCounter.ToString();
   }
}
