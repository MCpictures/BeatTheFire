using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerNameUI : MonoBehaviour
{
    [SerializeField] private GameObject firstSelectedButton;
    [SerializeField] GameObject playerNameCanvas;

    private void Awake()
    {
        playerNameCanvas.SetActive(false);
    }

    public void FocusButton()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
