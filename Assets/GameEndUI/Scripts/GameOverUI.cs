using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject focusedButton;
    [SerializeField] string mainMenuSceneName;

    public void GameOver()
    {
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(focusedButton);
        scoreText.text = "" + ScoreManager.Instance.CurrentScore;
    }

    public void ClickedMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ClickedRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
