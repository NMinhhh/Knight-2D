using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public enum ItemType
    {
        Health,
        Damage,
        Speed
    }

    public ItemType Type;

    public Sprite image;
    public int price;
    public string info;
}
