using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] string mainMenuSceneName;

    public void GameOver()
    {
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
        scoreText.text = "" + ScoreManager.Instance.CurrentScore;
    }

    public void ClickedMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
