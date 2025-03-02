using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ExperienceManager : Singleton<ExperienceManager>
{
    private Transform _player;
    private CharacterUnit _playerUnit;
    public List<int> expLevels;

    public List<Weapon> weaponsToUpgrade;

    public override void Init()
    {
        base.Init();

        _player = InstanceManager.Instance.Get(InstanceType.Player);
        _playerUnit = _player.GetComponent<CharacterUnit>();

        weaponsToUpgrade = new List<Weapon>();
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

        // 显示升级UI界面
        UIManager.Instance.levelUpPanel.SetActive(true);

        // 暂停游戏
        Time.timeScale = 0f;

        // 更新UI中按钮显示
        UpdateButtonDisplays();
    }

    private void UpdateButtonDisplays()
    {
        weaponsToUpgrade.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(PlayerController.Instance.assignedWeapons);

        // 已激活武器数小于最大武器数才显示可激活武器
        if (PlayerController.Instance.assignedWeapons.Count + PlayerController.Instance.fullyLevelWeapons.Count <
            CharacterManager.Instance.GetMaxWeapons())
        {
            availableWeapons.AddRange(PlayerController.Instance.unassignedWeapons);
        }

        for (int i = weaponsToUpgrade.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selected = Random.Range(0, availableWeapons.Count);
                weaponsToUpgrade.Add(availableWeapons[selected]);
                availableWeapons.RemoveAt(selected);
            }
        }

        for (int i = 0; i < weaponsToUpgrade.Count; i++)
        {
            UIManager.Instance.levelUpButtons[i].UpdateButtonDisplay(weaponsToUpgrade[i]);
        }

        // 只显示可升级的按钮
        for (int i = 0; i < UIManager.Instance.levelUpButtons.Length; i++)
        {
            if (i < weaponsToUpgrade.Count)
            {
                UIManager.Instance.levelUpButtons[i].gameObject.SetActive(true);
            }
            else
            {
                UIManager.Instance.levelUpButtons[i].gameObject.SetActive(false);
            }
        }
        
        CharacterStatManager.Instance.UpdateDisplay();
    }
}