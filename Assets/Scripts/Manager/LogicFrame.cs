using UnityEngine;

public class LogicFrame : MonoBehaviour
{
    private void Awake()
    {
        EventHandler.Init();
        InstanceManager.Instance.Init();
        FindInstance();
        
        CharacterManager.Instance.Init();
        DamageTextPool.Instance.Init();
        DamageTextPool.Instance.SetParent(transform.Find("DamageTextPool"));
        DamageTextManager.Instance.Init();
        DamageTextManager.Instance.SetCanvas(transform.Find("DamageCanvas"));
        ExperienceManager.Instance.Init();
        UIManager.Instance.Init();
        UIManager.Instance.SetCanvas(transform.Find("UI Canvas"));
        CharacterStatManager.Instance.Init();
        LevelManager.Instance.Init();
        CoinManager.Instance.Init();
    }

    private void Start()
    {
    }

    private void OnDestroy()
    {
        CoinManager.Instance.UnInit();
        LevelManager.Instance.UnInit();
        CharacterStatManager.Instance.UnInit();
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
        CharacterStatManager.Instance.Update();
        LevelManager.Instance.Update();
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
