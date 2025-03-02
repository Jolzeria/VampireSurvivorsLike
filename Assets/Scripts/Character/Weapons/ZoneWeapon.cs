using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneWeapon : Weapon
{
    public EnemyDamager enemyDamager;

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
    }

    public void SetStats()
    {
        enemyDamager.damageAmount = stats[weaponLevel].damage;
        enemyDamager.timeBetweenDamage = stats[weaponLevel].speed;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        enemyDamager.enableLifeTime = false;
    }
}