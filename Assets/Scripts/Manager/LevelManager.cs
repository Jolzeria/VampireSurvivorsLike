using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class LevelManager : Singleton<LevelManager>
{
    private bool gameActive;
    private float timer;

    public override void Init()
    {
        base.Init();

        ResetTimer();
        gameActive = true;
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public void Update()
    {
        if (gameActive)
        {
            timer -= Time.deltaTime;
            UIManager.Instance.UpdateTimer(timer);

            if (timer <= 0)
            {
                timer = 0f;
                UIManager.Instance.UpdateTimer(timer);
                EndGame(1);
            }
        }
    }

    public void ResetTimer()
    {
        timer = 600f;
    }

    public void EndGame(int type)
    {
        gameActive = false;

        CoroutineRunner.Instance.RunCoroutine(EndGameCo(type));
    }

    IEnumerator EndGameCo(int type)
    {
        if (type == 2)
            yield return new WaitForSeconds(1);

        Time.timeScale = 0f;
        if (type == 1)
            UIManager.Instance.ShowWinPanel();
        else if (type == 2)
            UIManager.Instance.ShowDeathPanel();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; // 仅在 Unity 编辑器中生效
#else
        Application.Quit(); // 在发布的应用程序中退出
#endif
    }

    public void PauseUnPause()
    {
        if (UIManager.Instance.pausePanel.activeSelf == false)
        {
            UIManager.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            UIManager.Instance.pausePanel.SetActive(false);
            if (!UIManager.Instance.levelUpPanel.activeSelf)
            {
                Time.timeScale = 1f;
            }
        }
    }
}