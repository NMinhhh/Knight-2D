using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeapons",menuName = "Weapons")]
public class WeaponObject : ScriptableObject
{
    public string nameWeapon = "name weapon";
    public Sprite image;
    public int damage;
    public int bullet;
    public float reload;
    public int price;
    public bool isPurchased = false;
    public float speed = 10f;
    public float timeLife = 3f;
    public float cooldown = 0.1f;
    public GameObject bulletIcon;
}
