using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveData 
{
    public int coin;
    public int diamond;
    public List<int> weaponsPurchasedIndex = new List<int>();
    public int selectedWeaponIndex;
    public List<int> mapUnlock = new List<int>();
    public List<int> mapWin = new List<int>();
    public List<int> avatarPurchaseIndex = new List<int>();
    public int selectedAvatarIndex;
}
