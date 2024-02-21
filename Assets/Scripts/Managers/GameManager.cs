using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    public int coin { get; private set; }

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
        SaveSystem.Init();
        Load();
        CanvasManager.Instance.SetCoinsUI(": "+coin);
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
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
        Load();
        CanvasManager.Instance.SetCoinsUI(": " + coin);

    }

    public void Load()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
            this.coin = saveObject.coin;
        }
    }

    public void PickupCoins(int amount)
    {
        coin += amount;
        Save();
    }

    class SaveObject
    {
        public int coin;
    }
}
