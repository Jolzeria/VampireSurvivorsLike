using System;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Transform _target;

    public float moveSpeed;

    public float damage;

    // 伤害间隔
    public float hitWaitTime = 1f;

    private float hitTimer;

    [Tooltip("怪物生命值")] public float health;

    [Tooltip("击退持续时间")] public float knockBackTime = .5f;
    private float knockBackTimer;

    public EnemyType enemyType;
    [Tooltip("经验值")] public int expValue = 1;
    [Tooltip("掉落金币数")] public int coinValue = 1;
    [Tooltip("掉落金币概率")] public float coinDropRate = .5f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = InstanceManager.Instance.Get(InstanceType.Player);
        hitTimer = hitWaitTime;

        var enemyUnit = GetComponent<EnemyUnit>();
        enemyUnit.health = health;
        enemyUnit.enemyType = enemyType;
        enemyUnit.expValue = expValue;
        enemyUnit.coinValue = coinValue;
        enemyUnit.coinDropRate = coinDropRate;
    }

    private void Update()
    {
        if (InstanceManager.Instance.Get(InstanceType.Player).gameObject.activeSelf)
        {
            // 击退
            if (knockBackTimer > 0)
            {
                knockBackTimer -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2f;
                }

                if (knockBackTimer <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * .5f);
                }
            }

            _rb.velocity = (Vector2) (_target.position - transform.position).normalized * moveSpeed;

            if (hitTimer > 0f)
            {
                hitTimer -= Time.deltaTime;
            }
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 对玩家造成伤害
        if (other.gameObject.CompareTag("Player") && hitTimer <= 0f)
        {
            var enemyUnit = GetComponent<BeUnit>();
            var damageInfo = new DamageInfo()
            {
                damage = damage,
                attacker = enemyUnit,
                receiver = other.collider.GetComponentInParent<BeUnit>()
            };
            EventHandler.ExecuteEvent(damageInfo.receiver, GameEventEnum.DamageProcess, damageInfo);

            hitTimer = hitWaitTime;
        }
    }

    public void KnockBack()
    {
        knockBackTimer = knockBackTime;
    }
}