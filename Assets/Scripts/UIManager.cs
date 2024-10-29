using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonBase<UIManager>
{
    public Canvas canvas;
    [SerializeField] private List<GameObject> panels;
    private GameObject _currentPanel;

    private void Start()
    {
        OpenPanel(panels[0].name);
    }

    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
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


