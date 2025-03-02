using System;
using UnityEngine;

public class GlobalHotkey : MonoBehaviour
{
    // 是否暂停
    public static bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    private void Update()
    {
        // 暂停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LevelManager.Instance.PauseUnPause();
        }
    }
}