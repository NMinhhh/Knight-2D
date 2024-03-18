using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
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

    public void FromJson(string json)
    {
    }
}
