using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : SingletonBase<LevelManager>
{
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
        StartCoroutine(RestartGame(1));
    }
    
    private  IEnumerator RestartGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
