using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndBtnFunc : MonoBehaviour
{
    public void GotoMainMenu()
    {
        LevelManager.Instance.GotoMainMenu();
    }

    public void Restart()
    {
        LevelManager.Instance.Restart();
    }
}
