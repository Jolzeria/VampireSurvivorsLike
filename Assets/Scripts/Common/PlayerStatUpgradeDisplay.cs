using TMPro;
using UnityEngine;

public class PlayerStatUpgradeDisplay : MonoBehaviour
{
    public TMP_Text valueText, costText;
    public GameObject upgradeButton;

    public void UpdateDisplay(int cost, float oldValue, float newValue)
    {
        valueText.text = "效果:" + oldValue.ToString("F1") + "->" + newValue.ToString("F1");
        costText.text = "花费:" + cost;

        // 判断金币够不够
        if (cost <= CoinManager.Instance.currentCoins)
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }
    }

    public void ShowMaxLevel(float value)
    {
        valueText.text = "效果:" + value.ToString("F1");
        costText.text = "已达最高等级";
        upgradeButton.SetActive(false);
    }
    
    public void PurchaseMoveSpeed()
    {
        CharacterStatManager.Instance.PurchaseMoveSpeed();
        ExperienceManager.Instance.SkipLevelUp();
    }

    public void PurchaseHealth()
    {
        CharacterStatManager.Instance.PurchaseHealth();
        ExperienceManager.Instance.SkipLevelUp();
    }

    public void PurchasePickupRange()
    {
        CharacterStatManager.Instance.PurchasePickupRange();
        ExperienceManager.Instance.SkipLevelUp();
    }

    public void PurchaseMaxWeapons()
    {
        CharacterStatManager.Instance.PurchaseMaxWeapons();
        ExperienceManager.Instance.SkipLevelUp();
    }
}