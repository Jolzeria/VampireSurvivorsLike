using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowerWeapon : Weapon
{
    public EnemyDamager enemyDamager;
    private float throwTimer;

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

        throwTimer -= Time.deltaTime;
        if (throwTimer <= 0)
        {
            throwTimer = stats[weaponLevel].timeBetweenAttacks;

            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                var obj = Instantiate(enemyDamager, enemyDamager.transform.position, enemyDamager.transform.rotation)
                    .gameObject;
                obj.SetActive(true);
            }
        }
    }

    public void SetStats()
    {
        enemyDamager.damageAmount = stats[weaponLevel].damage;
        enemyDamager.lifeTime = stats[weaponLevel].duration;
        enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        throwTimer = 0f;
    }
}