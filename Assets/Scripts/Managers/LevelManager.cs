using UnityEngine.SceneManagement;

public class LevelManager : SingletonBase<LevelManager>
{
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
