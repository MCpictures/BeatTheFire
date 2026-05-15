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
        EventSystem.current.SetSelectedGameObject(mainMenuButton);
        scoreText.text = "" + ScoreManager.Instance.CurrentScore;
    }

    public void ClickedMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
