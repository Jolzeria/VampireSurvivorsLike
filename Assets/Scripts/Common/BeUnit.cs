using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeUnit : MonoBehaviour
{
    protected BaseAttribute attribute;
    public BeUnit OriginalCreator { get; set; }

    private void Start()
    {
        Init();
    }

    private void OnDestroy()
    {
        //UnInit();
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnUpdate()
    {
    }

    protected virtual void Init()
    {
        RegisterEvent();
    }

    protected virtual void UnInit()
    {
        UnRegisterEvent();
    }

    protected virtual void RegisterEvent()
    {
    }

    protected virtual void UnRegisterEvent()
    {
    }

    public void AddAttrValue(AttributeType attr, float value)
    {
        attribute.AddAttrValue(attr, value);
    }

    public void MinusAttrValue(AttributeType attr, float value)
    {
        attribute.MinusAttrValue(attr, value);
    }

    public float GetAttrValue(AttributeType attr)
    {
        return attribute.GetAttrValue(attr);
    }

    public void SetAttrValue(AttributeType attr, float value)
    {
        attribute.SetAttrValue(attr, value);
    }

    public bool AttributeIsInit()
    {
        return attribute != null;
    }
}