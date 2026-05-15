using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class HighscoreEntry
{
    public string playerName;
    public int score;

    public HighscoreEntry(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}

[Serializable]
public class HighscoreData
{
    public List<HighscoreEntry> highscores = new();
}

public class ScoreManager : MonoBehaviour
{
    [SerializeField] GameStateManager gameStateManager;

    public static ScoreManager Instance;

    string playerName;
    int currentScore;
    int numberOfInnocentsInLevel;

    string savePath;
    HighscoreData highscoreData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        LoadHighscores();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Level1") return;

        currentScore = 0;
        if (gameStateManager == null)
        {
            gameStateManager = FindAnyObjectByType<GameStateManager>();
        }
    }

    public void Scored(int ScoreAmmount)
    {
        currentScore += ScoreAmmount;
        Debug.Log("New score: " + currentScore);
        numberOfInnocentsInLevel--;
        if (numberOfInnocentsInLevel == 0)
        {
            int timeScore = (int)(RoomManager.Instance.globalTimer * 60);
            currentScore += timeScore;
            gameStateManager.GameWin();
        }
    }

    public bool AddScoreIfHighscore()
    {
        if (highscoreData.highscores.Count >= 10)
        {
            int lowestHighscore = highscoreData.highscores[^1].score;

            if (currentScore <= lowestHighscore)
            {
                return false;
            }
        }

        highscoreData.highscores.Add(
            new HighscoreEntry(playerName, currentScore)
        );
        highscoreData.highscores.Sort(
            (a, b) => b.score.CompareTo(a.score)
        );
        if (highscoreData.highscores.Count > 10)
        {
            highscoreData.highscores.RemoveRange(
                10,
                highscoreData.highscores.Count - 10
            );
        }

        SaveHighscores();
        return true;
    }

    public List<HighscoreEntry> GetHighscores()
    {
        return highscoreData.highscores;
    }

    void SaveHighscores()
    {
        string json = JsonUtility.ToJson(highscoreData, true);
        File.WriteAllText(savePath, json);
    }

    void LoadHighscores()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            highscoreData = JsonUtility.FromJson<HighscoreData>(json);
        }
        else
        {
            highscoreData = new HighscoreData();
            SaveHighscores();
        }
    }

    public int CurrentScore { get { return currentScore; } }

    public string PlayerName { set { playerName = value; } }

    public int NumberOfInnocentsInLevel { set { numberOfInnocentsInLevel = value; } get { return numberOfInnocentsInLevel; } }
}
