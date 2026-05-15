using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject gameWinCanvas;
    [SerializeField] ScoreManager scoreManager;

    public void GameWin()
    {
        bool hasHighscore = ScoreManager.Instance.AddScoreIfHighscore();
        gameWinCanvas.SetActive(true);
        gameWinCanvas.GetComponent<GameWinUI>().GameWin(hasHighscore);
    }

    public void GameOver()
    {
        gameOverCanvas.SetActive(true);
        gameOverCanvas.GetComponent<GameOverUI>().GameOver();
    }
}
