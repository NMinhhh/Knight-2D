using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Shop/WeaponData")]
public class WeaponData : ScriptableObject
{
    public Weapon[] weapon;

    public int GetLength()
    {
        return weapon.Length;
    }

    public Weapon GetWeapon(int idx)
    {
        return weapon[idx];
    }

    public void WeaponPurchased(int idx)
    {
        weapon[idx].isPurchased = true;
    }
}
