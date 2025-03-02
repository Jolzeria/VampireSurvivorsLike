using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
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

        _player = null;
    }

    public Transform GetTransform()
    {
        return _player;
    }
    
    public CharacterUnit GetUnit()
    {
        return _playerUnit;
    }
    
    public float GetCurHp()
    {
        return _playerUnit.GetAttrValue(AttributeType.CurHp);
    }
    
    public float GetMaxHp()
    {
        return _playerUnit.GetAttrValue(AttributeType.MaxHp);
    }
    
    public int GetHpLevel()
    {
        return (int)_playerUnit.GetAttrValue(AttributeType.HpLevel);
    }

    public float GetMoveSpeed()
    {
        return _playerUnit.GetAttrValue(AttributeType.MoveSpeed);
    }
    
    public int GetMoveSpeedLevel()
    {
        return (int)_playerUnit.GetAttrValue(AttributeType.MoveSpeedLevel);
    }
    
    public float GetPickupRange()
    {
        return _playerUnit.GetAttrValue(AttributeType.PickupRange);
    }
    
    public int GetPickupRangeLevel()
    {
        return (int)_playerUnit.GetAttrValue(AttributeType.PickupRangeLevel);
    }
    
    public float GetMaxWeapons()
    {
        return _playerUnit.GetAttrValue(AttributeType.MaxWeapons);
    }
    
    public int GetMaxWeaponsLevel()
    {
        return (int)_playerUnit.GetAttrValue(AttributeType.MaxWeaponsLevel);
    }
}
