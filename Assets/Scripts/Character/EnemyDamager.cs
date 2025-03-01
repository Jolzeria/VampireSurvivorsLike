using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    public float damageAmount;

    public float lifeTime, growSpeed = 5f;
    private Vector3 targetSize;

    public bool shouldKnockBack;

    public bool destroyParent;
    
    void Start()
    {
        targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetSize, growSpeed * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            targetSize = Vector3.zero;

            if (transform.localScale.x == 0f)
            {
                Destroy(gameObject);
                
                if (destroyParent)
                    Destroy(transform.parent.gameObject);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 对怪物造成伤害
        if (other.gameObject.CompareTag("Enemy"))
        {
            var damageInfo = new DamageInfo()
            {
                damage = damageAmount,
                receiver = other.GetComponentInParent<BeUnit>(),
                shouldKnockBack = shouldKnockBack
            };
            EventHandler.ExecuteEvent(damageInfo.receiver, GameEventEnum.DamageProcess, damageInfo);
        }
    }
}
