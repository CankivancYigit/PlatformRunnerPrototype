using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> panels;
    private GameObject currentPanel;

    private void Start()
    {
        OpenPanel(panels[0].name);
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

        if (currentPanel != null)
        {
            currentPanel.SetActive(false);
        }

        currentPanel = panelToOpen;
        currentPanel.SetActive(true);
    }
}


