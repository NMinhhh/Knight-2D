using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {  get; private set; }

    public int coin;
    public List<int> weaponsPurchasedIndex = new List<int>();
    public int selectedWeaponIndex;

    Weapon weaponSelected;

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
        weaponsPurchasedIndex = new List<int>();
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

    public void SetWeaponSelected(Weapon weapon)
    {
        weaponSelected = weapon;
    }

    public Weapon GetWeaponSelected()
    {
        return weaponSelected;
    }

    //Weapon Index
    public int GetWeaponSelectedIndex()
    {
        return selectedWeaponIndex;
    }

    public void ChangeWeaponIndex(int idx)
    {
        selectedWeaponIndex = idx;
    }

    //Weapon Purchased
    public int GetWeaponPurchased(int idx)
    {
        return weaponsPurchasedIndex[idx];
    }

    public List<int> GetAllWeaponPurchased()
    {
        return weaponsPurchasedIndex;
    }

    public void AddWeaponPurchasedIndex(int idx)
    {
        weaponsPurchasedIndex.Add(idx);
    }

    //Json
    public void FromJson(string stringSave)
    {
        CoinData saveObj = JsonUtility.FromJson<CoinData>(stringSave);
        this.coin = saveObj.coin;
        this.weaponsPurchasedIndex = saveObj.weaponsPurchasedIndex;
        this.selectedWeaponIndex = saveObj.selectedWeaponIndex;
    }
}
