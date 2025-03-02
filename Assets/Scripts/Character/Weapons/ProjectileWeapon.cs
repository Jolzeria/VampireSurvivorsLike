using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWeapon : Weapon
{
    public EnemyDamager enemyDamager;
    public Projectile projectile;

    private float shotTimer;

    public float weaponRange;
    public LayerMask whatIsEnemy;

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

        shotTimer -= Time.deltaTime;
        if (shotTimer <= 0)
        {
            shotTimer = stats[weaponLevel].timeBetweenAttacks;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, weaponRange * stats[weaponLevel].range, whatIsEnemy);
            if (enemies.Length > 0)
            {
                for (int i = 0; i < stats[weaponLevel].amount; i++)
                {
                    // 随机向范围内的敌人射一根匕首
                    Vector3 targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                    Vector3 direction = targetPosition - transform.position;
                    // x轴与direction之间的夹角
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    angle -= 90;
                    // 绕z轴转向direction的角度
                    projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    
                    Instantiate(projectile, projectile.transform.position, projectile.transform.rotation).gameObject.SetActive(true);
                }
                
                SFXManager.instance.PlaySFXPitched(5);
            }
        }
    }

    public void SetStats()
    {
        enemyDamager.damageAmount = stats[weaponLevel].damage;
        enemyDamager.lifeTime = stats[weaponLevel].duration;
        // enemyDamager.transform.localScale = Vector3.one * stats[weaponLevel].range;

        projectile.moveSpeed = stats[weaponLevel].speed;
        
        shotTimer = 0f;
    }
}