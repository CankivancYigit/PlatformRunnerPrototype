using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RankingManager : MonoBehaviour
{
    public List<Transform> players;
    public List<TextMeshProUGUI> rankingTexts;
    
    void Update()
    {
        List<Transform> sortedPlayers = new List<Transform>(players);
        
        sortedPlayers.Sort((a, b) => b.position.z.CompareTo(a.position.z));
        
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            rankingTexts[i].text = sortedPlayers[i].name;
        }
    }
}





