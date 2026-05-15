using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedButton;
    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject playerNameCanvas;
    [SerializeField] TMP_Text playerNameText;
    [SerializeField] string LevelSceneName;

    [SerializeField] private TMP_Text hintText;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color blinkColor = Color.red;
    [SerializeField] private float blinkDuration = 3f;

    string playerName = "";
    bool isBlinking = false;

    private void Awake()
    {
        
    }

    void Start()
    {
        //playerNameCanvas.SetActive(false);
    }

    public void FocusButton()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    void ActivateBlink()
    {
        if (!isBlinking)
        {
            StartCoroutine(BlinkThreeTimes());
        }
    }

    IEnumerator BlinkThreeTimes()
    {
        isBlinking = true;

        for (int i = 0; i < 3; i++)
        {
            // Smooth to blink color
            yield return StartCoroutine(LerpColor(normalColor, blinkColor));

            // Smooth back
            yield return StartCoroutine(LerpColor(blinkColor, normalColor));
        }

        hintText.color = normalColor;
        isBlinking = false;
    }

    IEnumerator LerpColor(Color start, Color end)
    {
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / blinkDuration;

            hintText.color = Color.Lerp(start, end, t);

            yield return null;
        }

        hintText.color = end;
    }

    void AddLetter(string letter)
    {
        if (playerName.Length == 6) return;
        playerName += letter;
        playerNameText.text = playerName;
    }

    public void ClickedDel()
    {
        if (playerName.Length == 0) return;
        playerName = playerName.Substring(0, playerName.Length - 1);
        playerNameText.text = playerName;
    }

    public void ClickedBack()
    {
        playerName = "";
        playerNameText.text = playerName;
        mainMenuCanvas.SetActive(true);
        playerNameCanvas.SetActive(false);
    }

    public void ClickedStart()
    {
        if(playerName.Length < 4)
        {
            ActivateBlink();
        }
        else
        {
            ScoreManager.Instance.PlayerName = playerName;
            SceneManager.LoadScene(LevelSceneName);
        }
    }

    public void ClickedA()
    {
        AddLetter("A");
    }

    public void ClickedB()
    {
        AddLetter("B");
    }

    public void ClickedC()
    {
        AddLetter("C");
    }

    public void ClickedD()
    {
        AddLetter("D");
    }

    public void ClickedE()
    {
        AddLetter("E");
    }

    public void ClickedF()
    {
        AddLetter("F");
    }

    public void ClickedG()
    {
        AddLetter("G");
    }

    public void ClickedH()
    {
        AddLetter("H");
    }

    public void ClickedI()
    {
        AddLetter("I");
    }

    public void ClickedJ()
    {
        AddLetter("J");
    }

    public void ClickedK()
    {
        AddLetter("K");
    }

    public void ClickedL()
    {
        AddLetter("L");
    }

    public void ClickedM()
    {
        AddLetter("M");
    }

    public void ClickedN()
    {
        AddLetter("N");
    }

    public void ClickedO()
    {
        AddLetter("O");
    }

    public void ClickedP()
    {
        AddLetter("P");
    }

    public void ClickedQ()
    {
        AddLetter("Q");
    }

    public void ClickedR()
    {
        AddLetter("R");
    }

    public void ClickedS()
    {
        AddLetter("S");
    }

    public void ClickedT()
    {
        AddLetter("T");
    }

    public void ClickedU()
    {
        AddLetter("U");
    }

    public void ClickedV()
    {
        AddLetter("V");
    }

    public void ClickedW()
    {
        AddLetter("W");
    }

    public void ClickedX()
    {
        AddLetter("X");
    }

    public void ClickedY()
    {
        AddLetter("Y");
    }

    public void ClickedZ()
    {
        AddLetter("Z");
    }
}
