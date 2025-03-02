using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake()
    {
        Instance = this;
    }

    public float moveSpeed;

    private Animator _animator;
    private CharacterUnit _playerUnit;
    private Slider _healthSlider;

    public float pickupRange = 1.5f;

    // 未激活武器，已激活武器
    public List<Weapon> unassignedWeapons, assignedWeapons;
    // 满级武器
    [HideInInspector] public List<Weapon> fullyLevelWeapons;

    public int maxWeapons = 3;

    void Start()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _playerUnit = GetComponent<CharacterUnit>();
        _healthSlider = transform.Find("HealthCanvas/Slider").GetComponent<Slider>();
        fullyLevelWeapons = new List<Weapon>();

        StartCoroutine(WaitBeUnitInit());
    }

    IEnumerator WaitBeUnitInit()
    {
        while (!_playerUnit.AttributeIsInit())
        {
            yield return null;
        }

        _healthSlider.maxValue = _playerUnit.GetAttrValue(AttributeType.MaxHp);
        SetSliderValue();

        yield return new WaitForSeconds(1);
        if (assignedWeapons.Count == 0)
            AddWeapon(Random.Range(0, unassignedWeapons.Count));
    }

    void Update()
    {
        var moveX = Input.GetAxisRaw("Horizontal");
        var moveY = Input.GetAxisRaw("Vertical");
        var moveInput = new Vector3(moveX, moveY, 0);
        // 单位化，保证全向速度一致
        moveInput.Normalize();
        // 切换动画
        SwitchAnime(moveInput);

        transform.position += moveInput * moveSpeed * Time.deltaTime;
    }

    private void SwitchAnime(Vector3 move)
    {
        if (move == Vector3.zero)
        {
            _animator.SetInteger("movingStatus", 0);
            return;
        }

        if (Mathf.Abs(move.x) > Mathf.Abs(move.y))
        {
            if (move.x > 0)
                _animator.SetInteger("movingStatus", 4); // 向右移动
            else
                _animator.SetInteger("movingStatus", 3); // 向左移动
        }
        else
        {
            if (move.y > 0)
                _animator.SetInteger("movingStatus", 1); // 向上移动
            else
                _animator.SetInteger("movingStatus", 2); // 向下移动
        }
    }

    public void SetSliderValue()
    {
        _healthSlider.value = _playerUnit.GetAttrValue(AttributeType.CurHp);
    }

    public void AddWeapon(int weaponNumber)
    {
        if (weaponNumber < unassignedWeapons.Count)
        {
            assignedWeapons.Add(unassignedWeapons[weaponNumber]);
            unassignedWeapons[weaponNumber].gameObject.SetActive(true);

            unassignedWeapons.RemoveAt(weaponNumber);
        }
    }

    public void AddWeapon(Weapon weaponToAdd)
    {
        weaponToAdd.gameObject.SetActive(true);

        assignedWeapons.Add(weaponToAdd);
        unassignedWeapons.Remove(weaponToAdd);
    }
}