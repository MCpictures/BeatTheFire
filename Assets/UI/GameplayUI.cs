using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text globalTimerText;
    [SerializeField] private TMP_Text innocentsSavedText;
    private int totalInnocents;

    void Start()
    {
        Innocent[] innocents = FindObjectsByType<Innocent>();
        totalInnocents = innocents.Length;
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        globalTimerText.text = "Time Left: " + RoomManager.Instance.globalTimer.ToString("F2");
        innocentsSavedText.text = "Innocents Saved: " + (totalInnocents - ScoreManager.Instance.NumberOfInnocentsInLevel) + "/" + totalInnocents;
    }

    
}
