using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour, IInteractable
{
   public Transform wallPaintTransform;
   
   public void OnInteraction()
   {
      EventBus<PlayerReachedFinishEvent>.Emit(this,new PlayerReachedFinishEvent(wallPaintTransform));
   }
}
