using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Transform _UICanvas;
    private TMP_Text levelText;
    private Slider levelSlider;
    
    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();

        _UICanvas = null;
    }

    public void Update()
    {
        
    }

    public void SetCanvas(Transform trans)
    {
        _UICanvas = trans;
        levelText = _UICanvas.Find("Exp Level").GetComponent<TMP_Text>();
        levelSlider = _UICanvas.Find("Exp Bar").GetComponent<Slider>();
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLevel)
    {
        levelText.text = $"Level: {currentLevel}";

        levelSlider.maxValue = levelExp;
        levelSlider.value = currentExp;
    }
}
