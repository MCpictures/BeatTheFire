using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedButton;
    [SerializeField] PlayerNameUI playerNameUI;

    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject playerNameCanvas;
    [SerializeField] TMP_Text highscoreText;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        List<HighscoreEntry> highscores = ScoreManager.Instance.GetHighscores();
        string highscoresString = "";
        int i = 1;
        foreach (HighscoreEntry highscore in highscores)
        {
            highscoresString += i + ". " + highscore.playerName + " - " + highscore.score + "\n";
            i++;
        }
        highscoreText.text = highscoresString;
    }

    public void ClickedStartButton()
    {
        mainMenuCanvas.SetActive(false);
        playerNameCanvas.SetActive(true);
        playerNameUI.FocusButton();
    }

    public void ClickedExitButton()
    {
        Application.Quit();
    }
}
