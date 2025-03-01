using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    public int expValue;

    private bool isMovingToPlayer;
    public float moveSpeed;

    public float timeBetweenChecks = .2f;
    private float checkTimer;

    private Transform _playerTrans;
    private PlayerController _playerController;

    void Start()
    {
        _playerTrans = InstanceManager.Instance.Get(InstanceType.Player);
        _playerController = _playerTrans.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (isMovingToPlayer)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _playerTrans.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkTimer -= Time.deltaTime;
            if (checkTimer <= 0)
            {
                checkTimer = timeBetweenChecks;

                if (Vector3.Distance(transform.position, _playerTrans.position) < _playerController.pickupRange)
                {
                    isMovingToPlayer = true;
                    moveSpeed += _playerController.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ExperienceManager.Instance.GetExp(expValue);

            Destroy(gameObject);
        }
    }
}