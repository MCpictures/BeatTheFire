using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

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

    string playerName;
    int currentScore;
    int numberOfInnocentsInLevel;

    string savePath;
    HighscoreData highscoreData;

    void Awake()
    {
        savePath = Path.Combine(Application.persistentDataPath, "highscores.json");
        LoadHighscores();
    }

    void Start()
    {
        Innocent[] innocents = FindObjectsByType<Innocent>();
        numberOfInnocentsInLevel = innocents.Length;
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

    public void AddHighscore(string playerName, int score)
    {
        highscoreData.highscores.Add(
            new HighscoreEntry(playerName, score)
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
}
