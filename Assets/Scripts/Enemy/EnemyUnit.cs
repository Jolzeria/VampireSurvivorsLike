using System;
using UnityEngine;

public class EnemyUnit : BeUnit
{
    private EnemyController _enemyController;
    [HideInInspector] public EnemyType enemyType;
    [HideInInspector] public float health;
    [HideInInspector] public int expValue;

    protected override void Init()
    {
        base.Init();

        attribute = new EnemyAttribute();
        attribute.Init();

        attribute.SetAttrValue(AttributeType.MaxHp, (float) health);
        attribute.SetAttrValue(AttributeType.CurHp, (float) health);

        _enemyController = GetComponent<EnemyController>();
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
        var changedHp = damageInfo.damage;

        // 扣血
        MinusAttrValue(AttributeType.CurHp, changedHp);

        float curHp = GetAttrValue(AttributeType.CurHp);
        // 怪物死亡
        if (curHp <= 0)
        {
            // 生成经验
            ExperienceManager.Instance.SpawnExp(transform.position, enemyType, expValue);
            Destroy(gameObject);
            return;
        }

        // 控制击退
        if (damageInfo.shouldKnockBack)
        {
            _enemyController.KnockBack();
        }

        // 通知伤害跳字管理器
        DamageTextManager.Instance.Add(new DamageTextManager.DamageTextData()
            {position = damageInfo.receiver.transform.position, damage = Mathf.RoundToInt(changedHp)});
    }
}