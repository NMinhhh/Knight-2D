using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;
    public int coin { get; private set; }

    private List<int> amountGun;
    public int[] gunUnlock {  get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        amountGun = new List<int>();
        SaveSystem.Init();
        Load();
        amountGun.AddRange(gunUnlock);
        DontDestroyOnLoad(gameObject);
    }

    public void UseCoins(int amount)
    {
        coin -= amount;
        Save();
    }

    public bool HasEnoughCoins(int amount)
    {
        return coin >= amount;
    }

    public void Save()
    {
        SaveObject saveObject = new SaveObject()
        {
            coin = this.coin,
            amountGun = this.gunUnlock,
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
        CanvasManager.Instance.SetCoinsUI(": " + coin);

    }

    public void GetWeaponsUnLock(int i)
    {
        
        amountGun.Add(i);
        gunUnlock = amountGun.ToArray();
    }

    public void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            this.coin = saveObject.coin;
            //if(saveObject.amountGun != null)
            gunUnlock = saveObject.amountGun;
        }
        CanvasManager.Instance.SetCoinsUI(": " + coin);
    }

    public void PickupCoins(int amount)
    {
        coin += amount;
        Save();
    }

    public void OpenGun()
    {
       
    }

    class SaveObject
    {
        public int coin;
        public int[] amountGun = {0};
    }
}
