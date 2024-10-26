using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : SingletonBase<ScoreManager>
{
   private int _score = 0;
   
   public void UpdateScore(int amount)
   {
      _score += amount;
   }
}
