using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuFunc : MonoBehaviour
{
    public void StartGame()
    {
        LevelManager.Instance.StartGame();
    }

    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
    }
}