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

    public float maxEx {  get; private set; }

    private float currentEx;
    public int level {  get; private set; }

    PlayerStats exStats;

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
        level = 1;
        maxEx = CalculateExperience();
        amountGun = new List<int>();
        SaveSystem.Init();
        Load();
        amountGun.AddRange(gunUnlock);
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(currentEx >= maxEx)
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        level++;
        currentEx = 0;
        maxEx = CalculateExperience();
        CanvasManager.Instance.OpenMenuSkill();
        SelectionSkill.Instance.AppearMenuSkills();
    }

    public void UpdateEx(float ex)
    {
        currentEx += ex;
        exStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
        exStats.UpdateEx(currentEx);
    }

    int CalculateExperience()
    {
        int amountOfExperience = 0;
        for (int level = 1; level <= this.level; level++)
        {
            amountOfExperience += Mathf.FloorToInt(level + 200 * Mathf.Pow(2, (float)level / 7));
        }
        return amountOfExperience / 4;
        
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
