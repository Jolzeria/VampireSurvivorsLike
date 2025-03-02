using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterUnit : BeUnit
{
    public List<int> expLevels;
    public int levelCount = 100;

    public List<PlayerStatValue> moveSpeedUpgradeList;
    public List<PlayerStatValue> maxHealthUpgradeList;
    public List<PlayerStatValue> pickupRangeUpgradeList;
    public List<PlayerStatValue> maxWeaponsUpgradeList;

    protected override void Init()
    {
        base.Init();

        attribute = new CharacterAttribute();
        attribute.Init();

        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }

        ExperienceManager.Instance.expLevels = expLevels;

        CharacterStatManager.Instance.moveSpeedUpgradeList = moveSpeedUpgradeList;
        CharacterStatManager.Instance.maxHealthUpgradeList = maxHealthUpgradeList;
        CharacterStatManager.Instance.pickupRangeUpgradeList = pickupRangeUpgradeList;
        CharacterStatManager.Instance.maxWeaponsUpgradeList = maxWeaponsUpgradeList;
    }

    protected override void UnInit()
    {
        base.UnInit();

        attribute.UnInit();
    }

    protected override void OnUpdate()
    {
    }
    
    protected override void RegisterEvent()
    {
        base.RegisterEvent();
        
        EventHandler.RegisterEvent<DamageInfo>(this, GameEventEnum.DamageProcess, DamageProcess);
    }

    protected override void UnRegisterEvent()
    {
        base.UnRegisterEvent();
        
        EventHandler.UnRegisterEvent<DamageInfo>(this, GameEventEnum.DamageProcess, DamageProcess);
    }
    
    private void DamageProcess(DamageInfo damageInfo)
    {
        var damageNumber = damageInfo.damage;
        // 受伤
        if (damageNumber > 0)
        {
            MinusAttrValue(AttributeType.CurHp, damageNumber);
        } 
        // 回复
        else if (damageNumber < 0)
        {
            AddAttrValue(AttributeType.CurHp, damageNumber);
        }

        // 更新血条slider
        GetComponent<PlayerController>().SetSliderValue();
        
        float curHp = GetAttrValue(AttributeType.CurHp);
        if (curHp <= 0)
        {
            gameObject.SetActive(false);
            
            // 生成死亡粒子效果
            GameObject prefab = null;
            prefab = Resources.Load<GameObject>("Player Death Effect");
            var obj = GameObject.Instantiate(prefab, transform.position, transform.rotation);
            
            LevelManager.Instance.EndGame(2);
            
            SFXManager.instance.PlaySFX(3);
        }
    }
}

[Serializable]
public class PlayerStatValue
{
    public int cost;
    public float value;
}