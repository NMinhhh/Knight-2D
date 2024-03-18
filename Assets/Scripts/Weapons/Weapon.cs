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
    public float speed = 10f;
    public float timeLife = 3f;
    public float cooldown = 0.1f;
    public GameObject bulletIcon;
    public bool isPurchased;
    public bool isEquiped;
}
