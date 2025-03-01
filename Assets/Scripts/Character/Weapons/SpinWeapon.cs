using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    public float rotateSpeed;
    public Transform holder, fireballToSpawn;
    public float timeBetweenSpawn;
    private float spawnTimer;

    public EnemyDamager enemyDamager;

    void Start()
    {
        SetStats();
    }

    void Update()
    {
        holder.rotation = Quaternion.Euler(0f, 0f,
            holder.rotation.eulerAngles.z + (rotateSpeed * Time.deltaTime * stats[weaponLevel].speed));

        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = timeBetweenSpawn;

            var obj = Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder)
                .gameObject;
            obj.SetActive(true);
        }

        if (isStatsUpdated)
        {
            isStatsUpdated = false;
            
            SetStats();
        }
    }

    public void SetStats()
    {
        enemyDamager.damageAmount = stats[weaponLevel].damage;
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;
        enemyDamager.lifeTime = stats[weaponLevel].duration;
        spawnTimer = 0f;
    }
}