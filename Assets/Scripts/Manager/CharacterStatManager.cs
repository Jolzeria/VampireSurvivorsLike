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
            UIManager.Instance.pickupRandeUpgradeDisplay.UpdateDisplay(
                pickupRangeUpgradeList[pickupRangeLevel + 1].cost,
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

    public void PurchaseMoveSpeed()
    {
        var charUnit = CharacterManager.Instance.GetUnit();
        charUnit.AddAttrValue(AttributeType.MoveSpeedLevel, 1f);
        CoinManager.Instance.SpendCoins(moveSpeedUpgradeList[CharacterManager.Instance.GetMoveSpeedLevel()].cost);
        UpdateDisplay();

        charUnit.SetAttrValue(AttributeType.MoveSpeed,
            moveSpeedUpgradeList[CharacterManager.Instance.GetMoveSpeedLevel()].value);
    }

    public void PurchaseHealth()
    {
        var charUnit = CharacterManager.Instance.GetUnit();
        charUnit.AddAttrValue(AttributeType.HpLevel, 1f);
        CoinManager.Instance.SpendCoins(maxHealthUpgradeList[CharacterManager.Instance.GetHpLevel()].cost);
        UpdateDisplay();

        charUnit.SetAttrValue(AttributeType.MaxHp,
            maxHealthUpgradeList[CharacterManager.Instance.GetHpLevel()].value);
        charUnit.AddAttrValue(AttributeType.CurHp,
            (maxHealthUpgradeList[CharacterManager.Instance.GetHpLevel()].value -
             maxHealthUpgradeList[CharacterManager.Instance.GetHpLevel() - 1].value));
    }

    public void PurchasePickupRange()
    {
        var charUnit = CharacterManager.Instance.GetUnit();
        charUnit.AddAttrValue(AttributeType.PickupRangeLevel, 1f);
        CoinManager.Instance.SpendCoins(pickupRangeUpgradeList[CharacterManager.Instance.GetPickupRangeLevel()].cost);
        UpdateDisplay();

        charUnit.SetAttrValue(AttributeType.PickupRange,
            pickupRangeUpgradeList[CharacterManager.Instance.GetPickupRangeLevel()].value);
    }

    public void PurchaseMaxWeapons()
    {
        var charUnit = CharacterManager.Instance.GetUnit();
        charUnit.AddAttrValue(AttributeType.MaxWeaponsLevel, 1f);
        CoinManager.Instance.SpendCoins(maxWeaponsUpgradeList[CharacterManager.Instance.GetMaxWeaponsLevel()].cost);
        UpdateDisplay();

        charUnit.SetAttrValue(AttributeType.MaxWeapons,
            maxWeaponsUpgradeList[CharacterManager.Instance.GetMaxWeaponsLevel()].value);
    }
}