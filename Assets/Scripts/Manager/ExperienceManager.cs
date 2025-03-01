using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : Singleton<ExperienceManager>
{
    private Transform _player;
    private CharacterUnit _playerUnit;
    public List<int> expLevels;

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

    public void GetExp(int expValue)
    {
        _playerUnit.AddAttrValue(AttributeType.Exp, expValue);

        if (_playerUnit.GetAttrValue(AttributeType.Exp) >=
            expLevels[Mathf.RoundToInt(_playerUnit.GetAttrValue(AttributeType.Level))])
        {
            LevelUp();
        }

        UIManager.Instance.UpdateExperience(Mathf.RoundToInt(_playerUnit.GetAttrValue(AttributeType.Exp)),
            expLevels[Mathf.RoundToInt(_playerUnit.GetAttrValue(AttributeType.Level))],
            Mathf.RoundToInt(_playerUnit.GetAttrValue(AttributeType.Level)));
    }

    public void SpawnExp(Vector3 position, EnemyType enemyType, int expValue)
    {
        GameObject prefab = null;
        prefab = Resources.Load<GameObject>("Pickups/Experience Pickup");

        var obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
        obj.GetComponent<ExpPickup>().expValue = expValue;
        obj.SetActive(true);
    }

    private void LevelUp()
    {
        _playerUnit.MinusAttrValue(AttributeType.Exp,
            expLevels[Mathf.RoundToInt(_playerUnit.GetAttrValue(AttributeType.Level))]);

        _playerUnit.AddAttrValue(AttributeType.Level, 1f);

        if (_playerUnit.GetAttrValue(AttributeType.Level) >= expLevels.Count)
        {
            _playerUnit.SetAttrValue(AttributeType.Level, expLevels.Count - 1);
        }
    }
}