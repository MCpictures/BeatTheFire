using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGameListener : MonoBehaviour
{
    public static QuitGameListener Instance;
    InputAction quitAction;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            quitAction = InputSystem.actions.FindAction("UI/ExitGame");
            quitAction.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (quitAction.WasPressedThisFrame())
        {
            Debug.Log("Quit button pressed");
            Application.Quit();
        }
    }
}
