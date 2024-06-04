using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Coin
{
    public enum Type
    {
        Coin,
        Diamond
    }

    public Type type;

    public Sprite image;
    public int value;
    public int coinPrice;
    public float diamindPrice;
}
