using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    public Canvas canvas;
    [SerializeField] private List<GameObject> panels;
    public TextMeshProUGUI playerFinishRaceRankText;
    private GameObject _currentPanel;
    
    private void Start()
    {
        OpenPanel(panels[0].name);
    }

    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachedFinish);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachedFinish);
    }

    private void OnPlayerReachedFinish(object sender, PlayerReachedFinishEvent @event)
    {
        OpenPanel("Player Finish Race Panel");
        int playerFinishRaceRank = RankingManager.Instance.GetPlayerRank();
        playerFinishRaceRankText.text = "Player Finish Rank : " + playerFinishRaceRank;
    }

    private void OnPlayerReachedWallPaintingPos(object sender, PlayerReachedWallPaintingPosEvent @event)
    {
        OpenPanel("Wall Paint Panel");
    }

    private void OnLevelStart(object sender, LevelStartEvent @event)
    {
        OpenPanel("Gameplay Panel");
    }

    public void OpenPanel(string panelName)
    {
        GameObject panelToOpen = null;

        foreach (GameObject panel in panels)
        {
            if (panel.name == panelName)
            {
                panelToOpen = panel;
                break;
            }
        }

        if (panelToOpen == null)
        {
            Debug.LogError("Panel bulunamadı: " + panelName);
            return;
        }

        if (_currentPanel != null)
        {
            _currentPanel.SetActive(false);
        }

        _currentPanel = panelToOpen;
        _currentPanel.SetActive(true);
    }
}


