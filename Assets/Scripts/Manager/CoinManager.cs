using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : Singleton<CoinManager>
{
    public int currentCoins;

    public override void Init()
    {
        base.Init();
    }

    public override void UnInit()
    {
        base.UnInit();
    }

    public void AddCoins(int coinsNum)
    {
        currentCoins += coinsNum;

        UIManager.Instance.UpdateCoins();
    }

    public void DropCoin(Vector3 position, int coinAmount)
    {
        GameObject prefab = null;
        prefab = Resources.Load<GameObject>("Pickups/Coin Pickup");

        var obj = GameObject.Instantiate(prefab, position + new Vector3(.2f, .1f, 0f), Quaternion.identity);
        obj.GetComponent<CoinPickup>().coinAmount = coinAmount;
        obj.SetActive(true);
    }

    public void SpendCoins(int coinsToSpend)
    {
        currentCoins -= coinsToSpend;
        UIManager.Instance.UpdateCoins();
    }
}