using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunc : MonoBehaviour
{
    public void QuitGame()
    {
        LevelManager.Instance.QuitGame();
    }

    public void GoToMainMenu()
    {
        LevelManager.Instance.GotoMainMenu();
    }

    public void PauseUnPause()
    {
        LevelManager.Instance.PauseUnPause();
    }

    public void Restart()
    {
        LevelManager.Instance.Restart();
    }
}