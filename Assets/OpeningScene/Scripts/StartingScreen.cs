using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartingScreen : MonoBehaviour
{
    [SerializeField] PlayableDirector timeline;
    [SerializeField] string levelSceneName;

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            LoadMainScene();
        }
    }

    void OnEnable()
    {
        timeline.stopped += OnTimelineFinished;
    }

    void OnDisable()
    {
        timeline.stopped -= OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector director)
    {
        LoadMainScene();
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene(levelSceneName);
    }
}
