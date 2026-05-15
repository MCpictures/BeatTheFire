using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TMP_Text globalTimerText;

    void Update()
    {
        UpdateGlobalTimerText();
    }

    void UpdateGlobalTimerText()
    {
        globalTimerText.text = "Time Left: " + RoomManager.Instance.globalTimer.ToString("F2");
    }
}
