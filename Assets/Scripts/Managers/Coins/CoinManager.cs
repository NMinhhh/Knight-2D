using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {  get; private set; }

    public int coin;
    public Weapon[] weapons;
    public int selectedWeaponIndex;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    //Coin
    public void UseCoins(int amount)
    {
        coin -= amount;
    }

    public bool HasEnoughCoins(int amount)
    {
        return coin >= amount;
    }

    public void PickupCoins(int amount)
    {
        coin += amount;
    }

    //Weapons

    public void SetWeaponData(WeaponObject weapon)
    {
        weapons = weapon.weapons;
    }

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

    public void ChangeWeapon(int idx)
    {
        selectedWeaponIndex = idx;
    }

    public void FromJson(string stringSave)
    {
        CoinData saveObj = JsonUtility.FromJson<CoinData>(stringSave);
        this.coin = saveObj.coin;
        this.weapons = saveObj.weapons;
        this.selectedWeaponIndex = saveObj.selectedWeaponIndex;
    }
}
