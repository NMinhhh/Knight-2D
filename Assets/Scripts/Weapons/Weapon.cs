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
    public float widthImage;
    [Header("Bonus")]
    public float bonusMovementSpeedPercent = 0;
    public float bonusHealthPercent = 0;

    public float GetBunusMovementSpeedPercent()
    {
        return bonusHealthPercent / 100;
    }

    public float GetBunusHealthPercent()
    {
        return bonusHealthPercent / 100;
    }
}
