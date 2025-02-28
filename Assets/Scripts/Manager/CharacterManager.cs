using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : Singleton<CharacterManager>
{
    private Transform player;
    
    public override void Init()
    {
        base.Init();

        player = InstanceManager.Instance.Get(InstanceType.Player);
    }

    public override void UnInit()
    {
        base.UnInit();

        player = null;
    }

    public Transform GetTransform()
    {
        return player;
    }
}
