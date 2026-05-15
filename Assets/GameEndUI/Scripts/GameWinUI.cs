using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameWinUI : MonoBehaviour
{
    [SerializeField] GameObject highscoreText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject mainMenuButton;
    [SerializeField] string mainMenuSceneName;

    public void GameWin(bool hasHighscore)
    {
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
        if (hasHighscore)
        {
            highscoreText.SetActive(true);
        }
        scoreText.text = "" + ScoreManager.Instance.CurrentScore;
    }

    public void ClickedMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
