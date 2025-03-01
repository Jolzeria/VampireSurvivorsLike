using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : Singleton<ExperienceManager>
{
    private Transform _player;
    private CharacterUnit _playerUnit;

    public override void Init()
    {
        base.Init();
        
        _player = InstanceManager.Instance.Get(InstanceType.Player);
        _playerUnit = _player.GetComponent<CharacterUnit>();
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public void Update()
    {

    }

    public void GetExp(int amountToGet)
    {
        _playerUnit.GetExp(amountToGet);
    }

    public void SpawnExp(Vector3 position, EnemyType enemyType)
    {
        GameObject prefab = null;
        switch (enemyType)
        {
            case EnemyType.Enemy1_Bee:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy2_Slime:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy3_Scorpion:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy4_IceWolf:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy5_FireWolf:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy6_TreeMan:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            case EnemyType.Enemy7_Griffin:
                prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enemyType), enemyType, null);
        }

        var obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
        obj.SetActive(true);
    }
}