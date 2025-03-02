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
    
    public float GetCurHP()
    {
        return _playerUnit.GetAttrValue(AttributeType.CurHp);
    }
    
    public float GetMaxHP()
    {
        return _playerUnit.GetAttrValue(AttributeType.MaxHp);
    }

    public float GetMoveSpeed()
    {
        return _playerUnit.GetAttrValue(AttributeType.MoveSpeed);
    }
    
    public float GetPickupRange()
    {
        return _playerUnit.GetAttrValue(AttributeType.PickupRange);
    }
    
    public float GetMaxWeapons()
    {
        return _playerUnit.GetAttrValue(AttributeType.MaxWeapons);
    }
}
