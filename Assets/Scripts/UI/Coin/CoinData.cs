using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newCoinDatabase", menuName = "Shop/Coin Data Base")]
public class CoinData : ScriptableObject
{
    public Coin[] coins;

    public int GetLength() => coins.Length;

    public Coin GetCoin(int i) => coins[i];
}
