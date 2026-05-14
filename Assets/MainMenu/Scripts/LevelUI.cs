using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUI : MonoBehaviour
{
    [SerializeField] string LevelOneSceneName;

    public void OnLevelOneClicked()
    {
        SceneManager.LoadScene(LevelOneSceneName);
    }

    [SerializeField] string LevelTwoSceneName;

    public void OnLevelTwoClicked()
    {
        SceneManager.LoadScene(LevelOneSceneName);
    }

    [SerializeField] string LevelThreeSceneName;

    public void OnLevelThreeClicked()
    {
        SceneManager.LoadScene(LevelOneSceneName);
    }
}
