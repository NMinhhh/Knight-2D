using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {  get; private set; }

    public int coin;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void UseCoins(int amount)
    {
        coin -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return coin >= amount;
    }

    public void PickupCoins(int amount)
    {
        coin += amount;
    }

    public void FromJson(string stringSave)
    {
        CoinData saveObj = JsonUtility.FromJson<CoinData>(stringSave);
        this.coin = saveObj.coin;
    }
}
