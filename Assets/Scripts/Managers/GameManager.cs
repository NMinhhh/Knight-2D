using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Instance

    public static GameManager Instance;

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


        DontDestroyOnLoad(gameObject);
    }
    #endregion

    #region Variable
    //Coin
    public int coin { get; private set; }

    //Weapon
    private List<int> amountGun;
    public int[] gunUnlock {  get; private set; }

    //Level
    public float maxEx {  get; private set; }

    private float currentEx;
    public int level {  get; private set; }

    public bool isLevelUp {  get; private set; }

    PlayerStats exStats;

    //Time in game
    public int minutes { get; private set; }
    public int seconds { get; private set; }

    private float timer = 1;

    //Loss
    public bool isLoss {  get; private set; }

    public Weapon weaponObject;

    #endregion

    #region Unity Function
    
    private void Start()
    {
        //SaveSystem.Init();
        level = 1;
        isLevelUp = false;
        maxEx = CalculateExperience();
        //Load();
        //amountGun.AddRange(gunUnlock);
        
    }

    private void Update()
    {
        if(currentEx >= maxEx && !isLevelUp)
        {
            isLevelUp = true;
        }

        if(isLevelUp)
        {
            isLevelUp = false;
            LevelUp();
        }
        Timer();
     
    }

    #endregion

    #region Time in Game

    void Timer()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 1;
            seconds++;
            if (seconds == 60)
            {
                minutes++;
                seconds = 0;
            }
        }
    }

    #endregion

    #region Coins
    public void UseCoins(int amount)
    {
        coin -= amount;
        //Save();
    }

    public bool HasEnoughCoins(int amount)
    {
        return coin >= amount;
    }

    public void PickupCoins(int amount)
    {
        coin += amount;
        //Save();
    }

    #endregion

    #region Level Up

    public void LevelUp()
    {
        level++;
        currentEx = 0;
        maxEx = CalculateExperience();
        if (!SelectionSkill.Instance.isAllSkillFullLevel)
        {
            MenuSkillUI.Instance.OpenMenuSkill();
            SelectionSkill.Instance.AppearMenuSkills();
        }

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
            amountOfExperience += Mathf.FloorToInt(level + 300 * Mathf.Pow(2, (float)level / 7));
        }
        return amountOfExperience / 4;
        
    }

    #endregion

    #region Save and Load

    //public void Save()
    //{
    //    SaveObject saveObject = new SaveObject()
    //    {
    //        coin = this.coin,
    //        amountGun = this.gunUnlock,
    //        ak = this.weaponObject.ToString(),
    //    };
    //    string json = JsonUtility.ToJson(saveObject);
    //    SaveSystem.Save(json);

    //}
    //public void Load()
    //{
    //    string saveString = SaveSystem.Load();
    //    if (saveString != null)
    //    {
    //        SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);
    //        this.coin = saveObject.coin;
    //        gunUnlock = saveObject.amountGun;
            
    //    }
    //    else
    //    {
    //        GetWeaponsUnLock(0);
    //        Save();
    //    }
    //}
    class SaveObject
    {
        public int coin;
        public int[] amountGun;
        public string ak;
    }

    #endregion

    #region Weapons
    public void GetWeaponsUnLock(int i)
    {
        
        amountGun.Add(i);
        gunUnlock = amountGun.ToArray();
    }

    #endregion

    #region Loss
    public void GameStats()
    {
        isLoss = !isLoss;
    }
    #endregion

}
