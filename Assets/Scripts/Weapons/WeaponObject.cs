using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponDatabase",menuName = "Shop/WeaponDataBase")]
public class WeaponObject : ScriptableObject
{
    public Weapon[] weapons;
}
