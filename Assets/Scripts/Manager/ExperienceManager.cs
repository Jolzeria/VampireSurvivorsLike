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
}