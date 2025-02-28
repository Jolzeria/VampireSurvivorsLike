using UnityEngine;

public class LogicFrame : MonoBehaviour
{
    private void Awake()
    {
        EventHandler.Init();
        
        CharacterManager.Instance.Init();
        InstanceManager.Instance.Init();
        DamageTextPool.Instance.SetParent(transform.Find("DamageTextPool"));
        DamageTextManager.Instance.SetCanvas(transform.Find("DamageCanvas"));
        
        FindInstance();
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        DamageTextManager.Instance.UnInit();
        DamageTextPool.Instance.UnInit();
        InstanceManager.Instance.UnInit();
        CharacterManager.Instance.UnInit();
        
        EventHandler.UnInit();
    }

    private void Update()
    {
        DamageTextManager.Instance.Update();
    }

    private void FixedUpdate()
    {
    }

    private void FindInstance()
    {
        InstanceManager.Instance.Add(InstanceType.Player, GameObject.FindObjectOfType<PlayerController>().transform);
    }
}
