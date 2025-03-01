using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class CharacterUnit : BeUnit
{
    protected override void Init()
    {
        base.Init();

        attribute = new CharacterAttribute();
        attribute.Init();
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
        // 扣血
        MinusAttrValue(AttributeType.CurHp, damageInfo.damage);
        // 更新slider
        GetComponent<PlayerController>().SetSliderValue();
        
        float curHp = GetAttrValue(AttributeType.CurHp);
        if (curHp <= 0)
        {
            gameObject.SetActive(false);
            return;
        }
    }
    
    public void GetExp(int amountToGet)
    {
        AddAttrValue(AttributeType.Exp, amountToGet);
    }
}