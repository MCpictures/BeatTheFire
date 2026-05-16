using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGameListener : MonoBehaviour
{
    public static QuitGameListener Instance;
    InputAction quitAction;
    [SerializeField] private InputAction anyButtonPress;
    private float quitTimer = 0f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            quitAction = InputSystem.actions.FindAction("UI/ExitGame");
            quitAction.Enable();

            anyButtonPress.Enable();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {

        quitTimer += Time.deltaTime;
        print(quitTimer);

        if (anyButtonPress.triggered)
        {
            quitTimer = 0f;
        }

        if(quitTimer >= 180f)
        {
            Debug.Log("Quit");
            Application.Quit();
        }

        if (quitAction.WasPressedThisFrame())
        {
            Debug.Log("Quit button pressed");
            Application.Quit();
        }
    }
}
