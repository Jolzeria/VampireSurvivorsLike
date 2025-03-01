using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public List<WeaponStats> stats;
    public int weaponLevel;

    [HideInInspector] public bool isStatsUpdated;

    public Sprite icon;
    public string weaponName;

    public void LevelUp()
    {
        if (weaponLevel < stats.Count - 1)
        {
            weaponLevel++;

            isStatsUpdated = true;

            // 如果武器等级已满，就移入满级武器列表中
            if (weaponLevel >= stats.Count - 1)
            {
                PlayerController.Instance.fullyLevelWeapons.Add(this);
                PlayerController.Instance.assignedWeapons.Remove(this);
            }
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float speed;
    public float damage;
    public float range;
    public float timeBetweenAttacks;
    public float amount;
    public float duration;

    public string upgradeText;
}