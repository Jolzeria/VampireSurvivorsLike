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

    public GameObject levelUpPanel;
    public LevelUpSelectButton[] levelUpButtons;

    public PlayerStatUpgradeDisplay moveSpeedUpgradeDisplay;
    public PlayerStatUpgradeDisplay healthUpgradeDisplay;
    public PlayerStatUpgradeDisplay pickupRandeUpgradeDisplay;
    public PlayerStatUpgradeDisplay maxWeaponsUpgradeDisplay;

    private TMP_Text coinText;
    private TMP_Text timeText;

    private GameObject gameEndPanel;
    private TMP_Text deathText;
    private TMP_Text winText;

    public GameObject pausePanel;
    
    public override void Init()
    {
        base.Init();

        levelUpButtons = new LevelUpSelectButton[3];
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

        levelUpPanel = _UICanvas.Find("Level Up Interface").gameObject;
        levelUpButtons[0] = _UICanvas.Find("Level Up Interface/LevelUpButton1").GetComponent<LevelUpSelectButton>();
        levelUpButtons[1] = _UICanvas.Find("Level Up Interface/LevelUpButton2").GetComponent<LevelUpSelectButton>();
        levelUpButtons[2] = _UICanvas.Find("Level Up Interface/LevelUpButton3").GetComponent<LevelUpSelectButton>();

        coinText = _UICanvas.Find("Coin Text").GetComponent<TMP_Text>();
        timeText = _UICanvas.Find("Time Tetxt").GetComponent<TMP_Text>();

        moveSpeedUpgradeDisplay = _UICanvas.Find("Level Up Interface/PlayerStatUpgrade-MoveSpeed").GetComponent<PlayerStatUpgradeDisplay>();
        healthUpgradeDisplay = _UICanvas.Find("Level Up Interface/PlayerStatUpgrade-Health").GetComponent<PlayerStatUpgradeDisplay>();
        pickupRandeUpgradeDisplay = _UICanvas.Find("Level Up Interface/PlayerStatUpgrade-PickupRange").GetComponent<PlayerStatUpgradeDisplay>();
        maxWeaponsUpgradeDisplay = _UICanvas.Find("Level Up Interface/PlayerStatUpgrade-MaxWeapons").GetComponent<PlayerStatUpgradeDisplay>();

        gameEndPanel = _UICanvas.Find("Game End Panel").gameObject;
        deathText = gameEndPanel.transform.Find("DeathText").GetComponent<TMP_Text>();
        winText = gameEndPanel.transform.Find("WinText").GetComponent<TMP_Text>();

        pausePanel = _UICanvas.Find("Pause Panel").gameObject;
    }

    public void UpdateExperience(int currentExp, int levelExp, int currentLevel)
    {
        levelText.text = $"Level: {currentLevel}";

        levelSlider.maxValue = levelExp;
        levelSlider.value = currentExp;
    }

    public void UpdateCoins()
    {
        coinText.text = "金币：" + CoinManager.Instance.currentCoins;
    }

    public void UpdateTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / 60f);
        float seconds = Mathf.FloorToInt(time % 60f);

        timeText.text = "剩余时间: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ShowWinPanel()
    {
        winText.gameObject.SetActive(true);
        deathText.gameObject.SetActive(false);
        gameEndPanel.SetActive(true);
    }

    public void ShowDeathPanel()
    {
        winText.gameObject.SetActive(false);
        deathText.gameObject.SetActive(true);
        gameEndPanel.SetActive(true);
    }
}
