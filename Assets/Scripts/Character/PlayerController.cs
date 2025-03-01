using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;

    private Animator _animator;
    private BeUnit _charUnit;
    private Slider _healthSlider;
    
    void Start()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _charUnit = GetComponent<BeUnit>();
        _healthSlider = transform.Find("HealthCanvas/Slider").GetComponent<Slider>();

        StartCoroutine(WaitBeUnitInit());
    }

    IEnumerator WaitBeUnitInit()
    {
        while (!_charUnit.AttributeIsInit())
        {
            yield return null;
        }
        
        _healthSlider.maxValue = _charUnit.GetAttrValue(AttributeType.MaxHp);
        SetSliderValue();
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
        _healthSlider.value = _charUnit.GetAttrValue(AttributeType.CurHp);
    }
}
