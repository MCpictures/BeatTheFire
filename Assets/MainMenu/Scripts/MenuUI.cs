using UnityEngine;
using UnityEngine.EventSystems;

public class MenuUI : MonoBehaviour
{
    [SerializeField] GameObject firstSelectedButton;
    [SerializeField] PlayerNameUI playerNameUI;

    [SerializeField] GameObject mainMenuCanvas;
    [SerializeField] GameObject playerNameCanvas;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
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
