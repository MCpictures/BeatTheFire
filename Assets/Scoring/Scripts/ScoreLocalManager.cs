using UnityEngine;

public class ScoreLocalManager : MonoBehaviour
{
    void Start()
    {
        Innocent[] innocents = FindObjectsByType<Innocent>();
        ScoreManager.Instance.NumberOfInnocentsInLevel = innocents.Length;
    }
}
