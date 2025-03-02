using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }

    public void ResetTimer()
    {
        timer = 1200f;
    }

    public void EndGame()
    {
        
    }
}
