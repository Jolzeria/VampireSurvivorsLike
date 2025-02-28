using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType
{
    Bee
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyType spawnEnemyType;
    public float timeToSpawn;
    private float spawnCounter;
    public Transform minSpawn, maxSpawn;

    private Transform _target;

    private List<GameObject> spawnedEnemies;
    private float despawnDistance;

    public int checkPerFrame;
    private int enemyToCheck;

    void Start()
    {
        _target = InstanceManager.Instance.Get(InstanceType.Player);
        spawnCounter = timeToSpawn;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 5f;
        spawnedEnemies = new List<GameObject>();
        enemyToCheck = 0;
    }

    void Update()
    {
        if (spawnedEnemies == null)
            return;
        
        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0f)
        {
            spawnCounter = timeToSpawn;

            spawnedEnemies.Add(SpawnEnemy(spawnEnemyType));
        }

        // 刷怪点跟随玩家
        transform.position = _target.position;
        
        CheckDespawnEnemies();
    }

    private GameObject SpawnEnemy(EnemyType enemyType)
    {
        GameObject prefab = null;
        switch (enemyType)
        {
            case EnemyType.Bee:
                prefab = Resources.Load<GameObject>("Enemy_Bee");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
        }

        var obj = GameObject.Instantiate(prefab, GetSpawnPoint(), transform.rotation);
        obj.SetActive(true);

        return obj;
    }

    private Vector3 GetSpawnPoint()
    {
        var spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        // 50%在左右生成，50%在上下生成
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }

        return spawnPoint;
    }

    /// <summary>
    /// 每帧检查checkPerFrame个对象，超出距离就销毁
    /// </summary>
    private void CheckDespawnEnemies()
    {
        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if (enemyToCheck < spawnedEnemies.Count)
            {
                if (spawnedEnemies[enemyToCheck] != null)
                {
                    if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) >
                        despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);
                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }
}