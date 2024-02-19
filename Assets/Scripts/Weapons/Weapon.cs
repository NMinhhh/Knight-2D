using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Weapon 
{
    public string name;
    public Sprite image;
    public int damage;
    public int bullet;
    public float reload;
    public int price;
    public bool isPurchased = false;
}
