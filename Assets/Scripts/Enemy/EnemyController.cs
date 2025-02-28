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
    private float hitCounter;
    // 怪物生命值
    public float health;

    // 击退持续时间
    public float knockBackTime = .5f;
    private float knockBackCounter;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _target = InstanceManager.Instance.Get(InstanceType.Player);
        hitCounter = hitWaitTime;

        GetComponent<EnemyUnit>().health = health;
    }

    private void Update()
    {
        if (hitCounter > 0f)
        {
            hitCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        // 击退
        if (knockBackCounter > 0)
        {
            knockBackCounter -= Time.fixedDeltaTime;

            if (moveSpeed > 0)
            {
                moveSpeed = -moveSpeed * 2f;
            }

            if (knockBackCounter <= 0)
            {
                moveSpeed = Mathf.Abs(moveSpeed * .5f);
            }
        }
        
        _rb.velocity = (Vector2)(_target.position - transform.position).normalized * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 对玩家造成伤害
        if (other.gameObject.CompareTag("Player") && hitCounter <= 0f)
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

            hitCounter = hitWaitTime;
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackTime;
    }
}
