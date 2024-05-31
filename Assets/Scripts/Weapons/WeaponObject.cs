using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponDatabase",menuName = "Shop/WeaponDataBase")]
public class WeaponObject : ScriptableObject
{
    public Weapon[] weapons;

    public int GetWeaponsLength()
    {
        return weapons.Length;
    }

    public Weapon GetWeapon(int idx)
    {
        return weapons[idx];
    }

    public void WeaponPurchased(int idx)
    {
        weapons[idx].isPurchased = true;
    }

    public void ResetWeaponData()
    {
        foreach (var weapon in weapons)
        {
            weapon.isPurchased = false;
        }
    }
}
