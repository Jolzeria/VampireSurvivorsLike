using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyType
{
    Enemy1_Bee,
    Enemy2_Slime,
    Enemy3_Scorpion,
    Enemy4_IceWolf,
    Enemy5_FireWolf,
    Enemy6_TreeMan,
    Enemy7_Griffin
}

[System.Serializable]
public class WaveInfo
{
    public EnemyType enemyTypeToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}

public class EnemySpawner : MonoBehaviour
{
    // public EnemyType spawnEnemyType;
    // public float timeToSpawn;
    private float spawnTimer;
    public Transform minSpawn, maxSpawn;

    private Transform _target;

    private List<GameObject> spawnedEnemies;
    private float despawnDistance;

    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waveInfos;
    private int currentWave;
    private float waveTimer;

    void Start()
    {
        _target = InstanceManager.Instance.Get(InstanceType.Player);
        // spawnTimer = timeToSpawn;
        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position) + 5f;
        spawnedEnemies = new List<GameObject>();
        enemyToCheck = 0;

        currentWave = -1;
        GoToNextWave();
    }

    void Update()
    {
        /*spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0f)
        {
            spawnTimer = timeToSpawn;

            spawnedEnemies.Add(SpawnEnemy(spawnEnemyType));
        }*/

        if (InstanceManager.Instance.Get(InstanceType.Player).gameObject.activeSelf)
        {
            if (currentWave < waveInfos.Count)
            {
                waveTimer -= Time.deltaTime;
                if (waveTimer <= 0)
                {
                    GoToNextWave();
                }

                spawnTimer -= Time.deltaTime;
                if (spawnTimer <= 0)
                {
                    spawnTimer = waveInfos[currentWave].timeBetweenSpawns;
                    
                    spawnedEnemies.Add(SpawnEnemy(waveInfos[currentWave].enemyTypeToSpawn));
                }
            }
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
            case EnemyType.Enemy1_Bee:
                prefab = Resources.Load<GameObject>("Enemy1_Bee");
                break;
            case EnemyType.Enemy2_Slime:
                prefab = Resources.Load<GameObject>("Enemy2_Slime");
                break;
            case EnemyType.Enemy3_Scorpion:
                prefab = Resources.Load<GameObject>("Enemy3_Scorpion");
                break;
            case EnemyType.Enemy4_IceWolf:
                prefab = Resources.Load<GameObject>("Enemy4_IceWolf");
                break;
            case EnemyType.Enemy5_FireWolf:
                prefab = Resources.Load<GameObject>("Enemy5_FireWolf");
                break;
            case EnemyType.Enemy6_TreeMan:
                prefab = Resources.Load<GameObject>("Enemy6_TreeMan");
                break;
            case EnemyType.Enemy7_Griffin:
                prefab = Resources.Load<GameObject>("Enemy7_Griffin");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
        }

        var obj = GameObject.Instantiate(prefab, GetSpawnPoint(), Quaternion.identity);
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

    public void GoToNextWave()
    {
        currentWave++;

        if (currentWave >= waveInfos.Count)
        {
            currentWave = waveInfos.Count - 1;
        }

        waveTimer = waveInfos[currentWave].waveLength;
        spawnTimer = waveInfos[currentWave].timeBetweenSpawns;
    }
}