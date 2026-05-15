using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] ScoreManager scoreManager;

    public void GameWin()
    {
        int score = scoreManager.CurrentScore;
        int timeScore = (int)RoomManager.Instance.globalTimer * 60;
        score += timeScore;

        Debug.Log("You rescued all survivors and win. Your score: " + score);
    }

    public void GameOver()
    {
        int score = scoreManager.CurrentScore;
        Debug.Log("You lost. Your score: " + score);
    }
}
