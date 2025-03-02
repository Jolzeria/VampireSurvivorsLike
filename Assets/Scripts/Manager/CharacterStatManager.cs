using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatManager : Singleton<CharacterStatManager>
{
    public List<PlayerStatValue> moveSpeedUpgradeList;
    public List<PlayerStatValue> maxHealthUpgradeList;
    public List<PlayerStatValue> pickupRangeUpgradeList;
    public List<PlayerStatValue> maxWeaponsUpgradeList;

    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public void Update()
    {
        if (UIManager.Instance.levelUpPanel.activeSelf)
        {
            UpdateDisplay();
        }
    }

    /// <summary>
    /// 更新升级角色属性按钮
    /// </summary>
    public void UpdateDisplay()
    {
        var moveSpeedLevel = CharacterManager.Instance.GetMoveSpeedLevel();
        if (moveSpeedLevel < moveSpeedUpgradeList.Count - 1)
        {
            UIManager.Instance.moveSpeedUpgradeDisplay.UpdateDisplay(moveSpeedUpgradeList[moveSpeedLevel + 1].cost,
                moveSpeedUpgradeList[moveSpeedLevel].value, moveSpeedUpgradeList[moveSpeedLevel + 1].value);
        }
        else
        {
            UIManager.Instance.moveSpeedUpgradeDisplay.ShowMaxLevel(CharacterManager.Instance.GetMoveSpeed());
        }
        
        var hpLevel = CharacterManager.Instance.GetHpLevel();
        if (hpLevel < maxHealthUpgradeList.Count - 1)
        {
            UIManager.Instance.healthUpgradeDisplay.UpdateDisplay(maxHealthUpgradeList[hpLevel + 1].cost,
                maxHealthUpgradeList[hpLevel].value, maxHealthUpgradeList[hpLevel + 1].value);
        }
        else
        {
            UIManager.Instance.healthUpgradeDisplay.ShowMaxLevel(CharacterManager.Instance.GetMaxHp());
        }
        
        var pickupRangeLevel = CharacterManager.Instance.GetPickupRangeLevel();
        if (hpLevel < pickupRangeUpgradeList.Count - 1)
        {
            UIManager.Instance.pickupRandeUpgradeDisplay.UpdateDisplay(pickupRangeUpgradeList[pickupRangeLevel + 1].cost,
                pickupRangeUpgradeList[pickupRangeLevel].value, pickupRangeUpgradeList[pickupRangeLevel + 1].value);
        }
        else
        {
            UIManager.Instance.pickupRandeUpgradeDisplay.ShowMaxLevel(CharacterManager.Instance.GetPickupRange());
        }
        
        var maxWeaponsLevel = CharacterManager.Instance.GetMaxWeaponsLevel();
        if (hpLevel < maxWeaponsUpgradeList.Count - 1)
        {
            UIManager.Instance.maxWeaponsUpgradeDisplay.UpdateDisplay(maxWeaponsUpgradeList[maxWeaponsLevel + 1].cost,
                maxWeaponsUpgradeList[maxWeaponsLevel].value, maxWeaponsUpgradeList[maxWeaponsLevel + 1].value);
        }
        else
        {
            UIManager.Instance.maxWeaponsUpgradeDisplay.ShowMaxLevel(CharacterManager.Instance.GetMaxWeapons());
        }
    }
}