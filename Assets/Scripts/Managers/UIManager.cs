using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : SingletonBase<UIManager>
{
    public Canvas canvas;
    [SerializeField] private List<GameObject> panels;
    public TextMeshProUGUI playerFinishRaceRankText;
    public Button restartButton;
    private GameObject _currentPanel;
    
    private void Start()
    {
        OpenPanel(panels[0].name);
        restartButton.onClick.AddListener(RestartLevel);
    }
    
    private void OnEnable()
    {
        EventBus<LevelStartEvent>.AddListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.AddListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerReachedFinishEvent>.AddListener(OnPlayerReachedFinish);
        EventBus<WallPaintFinishEvent>.AddListener(OnWallPaintFinish);
    }

    private void OnDisable()
    {
        EventBus<LevelStartEvent>.RemoveListener(OnLevelStart);
        EventBus<PlayerReachedWallPaintingPosEvent>.RemoveListener(OnPlayerReachedWallPaintingPos);
        EventBus<PlayerReachedFinishEvent>.RemoveListener(OnPlayerReachedFinish);
        EventBus<WallPaintFinishEvent>.RemoveListener(OnWallPaintFinish);
    }

    private void OnWallPaintFinish(object sender, WallPaintFinishEvent @event)
    {
        OpenPanel("Restart Panel");
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
    
    private void RestartLevel()
    {
        LevelManager.Instance.RestartGame();
    }
}


