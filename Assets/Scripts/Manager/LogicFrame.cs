using UnityEngine;

public class LogicFrame : MonoBehaviour
{
    private void Awake()
    {
        EventHandler.Init();
        InstanceManager.Instance.Init();
        FindInstance();
        
        CharacterManager.Instance.Init();
        DamageTextPool.Instance.SetParent(transform.Find("DamageTextPool"));
        DamageTextManager.Instance.SetCanvas(transform.Find("DamageCanvas"));
        UIManager.Instance.SetCanvas(transform.Find("UI Canvas"));
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        UIManager.Instance.UnInit();
        ExperienceManager.Instance.UnInit();
        DamageTextManager.Instance.UnInit();
        DamageTextPool.Instance.UnInit();
        CharacterManager.Instance.UnInit();
        
        InstanceManager.Instance.UnInit();
        EventHandler.UnInit();
    }

    private void Update()
    {
        DamageTextManager.Instance.Update();
        UIManager.Instance.Update();
    }

    private void FixedUpdate()
    {
    }

    private void FindInstance()
    {
        InstanceManager.Instance.Add(InstanceType.Player, GameObject.FindObjectOfType<PlayerController>().transform);
        InstanceManager.Instance.Add(InstanceType.UICavnas, transform.Find("UI Canvas"));
    }
}
