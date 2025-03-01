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
    // 怪物生命值
    public float health;

    // 击退持续时间
    public float knockBackTime = .5f;
    private float knockBackTimer;

    public EnemyType enemyType;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = InstanceManager.Instance.Get(InstanceType.Player);
        hitTimer = hitWaitTime;

        GetComponent<EnemyUnit>().health = health;
        GetComponent<EnemyUnit>().enemyType = enemyType;
    }

    private void Update()
    {
        if (hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // 击退
        if (knockBackTimer > 0)
        {
            knockBackTimer -= Time.fixedDeltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackTimer <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
        
        _rb.velocity = (Vector2)(_target.position - transform.position).normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 对玩家造成伤害
        if (other.gameObject.CompareTag("Player") && hitTimer <= 0f)
        {
            var enemyUnit = GetComponent<BeUnit>();
            var snapShot = new SnapShot(enemyUnit);
            var damageInfo = new DamageInfo()
            {
                damage = damage,
                snapShot = snapShot,
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
