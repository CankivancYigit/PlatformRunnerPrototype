using TMPro;
using UnityEngine;

public class FailureCounter : MonoBehaviour
{
    public TextMeshProUGUI failureCounterText;
    private int _failureCount;
    private string _failureKey = "FailureCount";

    private void OnEnable()
    {
        EventBus<PlayerCollidedEvent>.AddListener(OnPlayerCollide);
    }

    private void OnDisable()
    {
        EventBus<PlayerCollidedEvent>.RemoveListener(OnPlayerCollide);
    }

    private void OnPlayerCollide(object sender, PlayerCollidedEvent @event)
    {
        OnPlayerFailed();
    }

    void Start()
    {
        _failureCount = PlayerPrefs.GetInt(_failureKey, 0);
        failureCounterText.text = "Fail Count: " + _failureCount;
    }

    public void OnPlayerFailed()
    {
        _failureCount++;
        PlayerPrefs.SetInt(_failureKey, _failureCount);
        failureCounterText.text = "Fail Count: " + _failureCount;
    }

    public void ResetFailures()
    {
        _failureCount = 0;
        PlayerPrefs.SetInt(_failureKey, _failureCount);
    }
}

