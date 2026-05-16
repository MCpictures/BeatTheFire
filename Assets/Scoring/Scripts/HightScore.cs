using TMPro;
using UnityEngine;

public class HightScore : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private float ground;
    void Update()
    {

        float height = player.position.y - ground;
        height = Mathf.Max(0f, height); // never show negative
        heightText.text = height.ToString("F1") + "m";
    }
}
