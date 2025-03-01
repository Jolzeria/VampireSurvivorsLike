using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelectButton : MonoBehaviour
{
    public TMP_Text upgradeDescText, nameText;
    public Image weaponIcon;

    private Weapon _assignedWeapon;

    /// <summary>
    /// 更新按钮的显示
    /// </summary>
    /// <param name="theWeapon">武器对象</param>
    public void UpdateButtonDisplay(Weapon theWeapon)
    {
        if (theWeapon.gameObject.activeSelf)
        {
            upgradeDescText.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
            weaponIcon.sprite = theWeapon.icon;
            nameText.text = theWeapon.weaponName + " - Lvl " + theWeapon.weaponLevel;
        }
        else
        {
            upgradeDescText.text = "解锁 " + theWeapon.weaponName;
            weaponIcon.sprite = theWeapon.icon;
            nameText.text = theWeapon.weaponName;
        }

        _assignedWeapon = theWeapon;
    }

    /// <summary>
    /// 点击按钮完成对应武器的升级
    /// </summary>
    public void SelectUpgrade()
    {
        if (_assignedWeapon != null)
        {
            if (_assignedWeapon.gameObject.activeSelf)
            {
                _assignedWeapon.LevelUp();
            }
            else
            {
                PlayerController.Instance.AddWeapon(_assignedWeapon);
            }

            UIManager.Instance.levelUpPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}