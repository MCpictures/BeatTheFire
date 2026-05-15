using UnityEditor;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;

    int currentScore;
    public int numberOfInnocentsInLevel;

    void Start()
    {
        Innocent[] innocents = FindObjectsByType<Innocent>();
        numberOfInnocentsInLevel = innocents.Length;
        // Load highscores from save file
    }

    public void Scored(int ScoreAmmount)
    {
        currentScore += ScoreAmmount;
        Debug.Log("New score: " + currentScore);
        numberOfInnocentsInLevel--;
        if(numberOfInnocentsInLevel == 0)
        {
            gameStateManager.GameWin();
        }
    }

    public int CurrentScore { get { return currentScore; } }
}
