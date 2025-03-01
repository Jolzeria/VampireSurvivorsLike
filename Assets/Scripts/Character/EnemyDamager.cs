using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    // 造成伤害量
    public float damageAmount = 5f;
    // 武器存活时长
    public bool enableLifeTime = false;
    public float lifeTime = 5f;
    // 武器创建消失的渐变速度
    public float growSpeed = 5f;
    private Vector3 _targetSize;

    [Tooltip("是否击退")] public bool shouldKnockBack;
    [Tooltip("是否销毁父组件")] public bool destroyParent;

    [Tooltip("是否持续伤害")] public bool isContinuousDamage = false;
    [Tooltip("两次持续伤害间隔")] public float timeBetweenDamage;
    private float _continuousDamageTimer;
    private List<EnemyUnit> _enemiesInRange;

    void Start()
    {
        _targetSize = transform.localScale;
        transform.localScale = Vector3.zero;

        _enemiesInRange = new List<EnemyUnit>();
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growSpeed * Time.deltaTime);

        if (enableLifeTime)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                _targetSize = Vector3.zero;

                if (transform.localScale.x == 0f)
                {
                    Destroy(gameObject);

                    if (destroyParent)
                        Destroy(transform.parent.gameObject);
                }
            }
        }

        if (isContinuousDamage)
        {
            _continuousDamageTimer -= Time.deltaTime;

            if (_continuousDamageTimer <= 0)
            {
                _continuousDamageTimer = timeBetweenDamage;

                for (int i = 0; i < _enemiesInRange.Count; i++)
                {
                    if (_enemiesInRange[i] != null)
                    {
                        var damageInfo = new DamageInfo()
                        {
                            damage = damageAmount,
                            receiver = _enemiesInRange[i],
                            shouldKnockBack = shouldKnockBack
                        };
                        EventHandler.ExecuteEvent(damageInfo.receiver, GameEventEnum.DamageProcess, damageInfo);
                    }
                    else
                    {
                        _enemiesInRange.RemoveAt(i);
                        i--;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 对怪物造成伤害
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (!isContinuousDamage)
            {
                var damageInfo = new DamageInfo()
                {
                    damage = damageAmount,
                    receiver = other.GetComponentInParent<BeUnit>(),
                    shouldKnockBack = shouldKnockBack
                };
                EventHandler.ExecuteEvent(damageInfo.receiver, GameEventEnum.DamageProcess, damageInfo);
            }
            else
            {
                if (other.gameObject.CompareTag("Enemy"))
                {
                    _enemiesInRange.Add(other.GetComponent<EnemyUnit>());
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (isContinuousDamage)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                _enemiesInRange.Remove(other.GetComponent<EnemyUnit>());
            }
        }
    }
}