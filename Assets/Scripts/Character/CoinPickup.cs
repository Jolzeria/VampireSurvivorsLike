using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [HideInInspector]
    public int coinAmount = 1;

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

                if (Vector3.Distance(transform.position, _playerTrans.position) < CharacterManager.Instance.GetPickupRange())
                {
                    isMovingToPlayer = true;
                    moveSpeed += CharacterManager.Instance.GetMoveSpeed();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CoinManager.Instance.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}