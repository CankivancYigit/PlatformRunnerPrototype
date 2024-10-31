using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : SingletonBase<FinishLine>, IInteractable
{
   public Transform wallPaintTransform;
   public Transform opponentFinishTransform;
   
   public void OnInteraction()
   {
      EventBus<PlayerReachedFinishEvent>.Emit(this,new PlayerReachedFinishEvent(wallPaintTransform));
   }
}
