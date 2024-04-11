using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {  get; private set; }

    public int coin;

    public int diamond;
    
    public List<int> weaponsPurchasedIndex = new List<int>();
    
    public int selectedWeaponIndex;
    
    Weapon weaponSelected;

    public List<int> mapUnlock = new List<int>();

    public List<int> mapWin = new List<int>();

    int selectedMap;

    bool mapState;

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

    //Diamond
    public void UseDiamond(int amount)
    {
        diamond -= amount;
    }

    public bool HasEnenoughDiamond(int amount)
    {
        return coin >= amount;
    }

    public void PickUpDiamond(int amount)
    {
        diamond += amount;
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

    //Map
    public List<int> GetAllMapUnlock()
    {
        return mapUnlock;
    }

    public void AddMapUnlock(int level)
    {
        mapUnlock.Add(level);
    }

    public int GetMapUnlock(int level)
    {
        return mapUnlock[level];
    }

    public List<int> GetAllMapWin()
    {
        return mapWin;
    }

    public void AddMapWin(int level)
    {
        mapWin.Add(level);
    }

    public int GetMapWin(int level)
    {
        return mapWin[level];
    }

    public void SetMapState(bool state)
    {
        mapState = state;
    }

    public bool GetMapState()
    {
        return mapState;
    }

    public void SetSelectedMap(int selectedMap)
    {
        this.selectedMap = selectedMap;
    }

    public int GetSelectedMap()
    {
        return selectedMap;
    }

    //Json
    public void FromJson(string stringSave)
    {
        CoinData saveObj = JsonUtility.FromJson<CoinData>(stringSave);
        this.coin = saveObj.coin;
        this.diamond = saveObj.diamond;
        this.weaponsPurchasedIndex = saveObj.weaponsPurchasedIndex;
        this.selectedWeaponIndex = saveObj.selectedWeaponIndex;
        this.mapUnlock = saveObj.mapUnlock;
        this.mapWin = saveObj.mapWin;
    }
}
