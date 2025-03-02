using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    public EnemyDamager enemyDamager;

    private float attackTimer;
    private float direction;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        if (isStatsUpdated)
        {
            isStatsUpdated = false;

            SetStats();
        }

        direction = Input.GetAxisRaw("Horizontal");
        if (direction != 0)
        {
            if (direction > 0)
            {
                enemyDamager.transform.rotation = Quaternion.identity;
            }
            else
            {
                enemyDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
            }
        }

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            attackTimer = stats[weaponLevel].timeBetweenAttacks;


            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rotZ = 360f / stats[weaponLevel].amount * i;

                var obj = Instantiate(enemyDamager, enemyDamager.transform.position,
                    Quaternion.Euler(0f, 0f, enemyDamager.transform.rotation.eulerAngles.z + rotZ),
                    transform).gameObject;
                obj.SetActive(true);
            }
            
            SFXManager.instance.PlaySFXPitched(7);
        }
    }

    public void SetStats()
    {
        enemyDamager.damageAmount = stats[weaponLevel].damage;
        enemyDamager.lifeTime = stats[weaponLevel].duration;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        attackTimer = 0f;
    }
}