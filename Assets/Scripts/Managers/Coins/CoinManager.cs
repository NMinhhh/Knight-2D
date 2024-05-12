using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance {  get; private set; }

    public int coin;

    public int diamond;
    
    //Weapon
    public List<int> weaponsPurchasedIndex = new List<int>();
    
    public int selectedWeaponIndex;
    
    Weapon weaponSelected;

    //Map
    public List<int> mapUnlock = new List<int>();

    public List<int> mapWin = new List<int>();

    int selectedMap;

    bool mapState;

    //Avatar
    public List<int> avatarPurchaseIndex = new List<int>();

    public int selectedAvatarIndex;

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
        return diamond >= amount;
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

    public void AddWeaponPurchasedIndex(int id)
    {
        weaponsPurchasedIndex.Add(id);
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

    public int GetMapUnlock(int idx)
    {
        return mapUnlock[idx];
    }

    public List<int> GetAllMapWin()
    {
        return mapWin;
    }

    public void AddMapWin(int level)
    {
        mapWin.Add(level);
    }

    public int GetMapWin(int idx)
    {
        return mapWin[idx];
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

    //Avatar

    public int GetSelectedAvatarIndex()
    {
        return selectedAvatarIndex;
    }

    public void ChangeAvatarIndex(int id)
    {
        selectedAvatarIndex = id;
    }

    public List<int> GetAllAvatarPurchasedIndex()
    {
        return avatarPurchaseIndex;
    }

    public void AddAvatarPurchased(int id)
    {
        avatarPurchaseIndex.Add(id);
    }

    public int GetAvatarPurchased(int idx)
    {
        return avatarPurchaseIndex[idx]; 
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
        this.avatarPurchaseIndex = saveObj.avatarPurchaseIndex;
        this.selectedAvatarIndex = saveObj.selectedAvatarIndex;
    }
}
