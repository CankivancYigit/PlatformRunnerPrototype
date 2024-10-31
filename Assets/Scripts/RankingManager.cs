using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingManager : SingletonBase<RankingManager>
{
    public List<Transform> players;
    public List<TextMeshProUGUI> rankingTexts;

    private int _playerRank;

    void Update()
    {
        List<Transform> sortedPlayers = new List<Transform>(players);
        sortedPlayers.Sort((a, b) => b.position.z.CompareTo(a.position.z));

        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            rankingTexts[i].text = sortedPlayers[i].name;
            if (sortedPlayers[i].name == "Player")
            {
                _playerRank = i + 1;
            }
        }
    }

    public int GetPlayerRank()
    {
        return _playerRank;
    }
}






