using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrownWeapon : MonoBehaviour
{
    [Tooltip("投掷力度")] public float throwPower;
    [Tooltip("旋转速度")] public float rotateSpeed;
    private Rigidbody2D _theRb;

    void Start()
    {
        _theRb = GetComponent<Rigidbody2D>();

        _theRb.velocity = new Vector2(Random.Range(-4, 4), Random.Range(throwPower * 0.8f, throwPower * 1.2f));
    }

    void Update()
    {
        transform.rotation =
            Quaternion.Euler(0f, 0f,
                transform.rotation.eulerAngles.z +
                (rotateSpeed * 360f * Time.deltaTime * -Mathf.Sign(_theRb.velocity.x)));
    }
}