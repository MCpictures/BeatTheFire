using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text globalTimerText;
    [SerializeField] private Color timerFlashColor = Color.red;
    [SerializeField] private float timerFlashDuration = 0.1f;

    void Update()
    {
        UpdateGlobalTimerText();
    }

    void UpdateGlobalTimerText()
    {
        globalTimerText.text = "Time Left: " + RoomManager.Instance.globalTimer.ToString("F2");
    }
}
