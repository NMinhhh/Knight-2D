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


    }
    #endregion

    #region Variable

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


    public int kill {  get; private set; }

    #endregion

    #region Unity Function
    
    private void Start()
    {
        //SaveSystem.Init();
        level = 1;
        isLevelUp = false;
        maxEx = CalculateExperience();
        exStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
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



    #region Level Up

    public void LevelUp()
    {
        level++;
        currentEx = 0;
        exStats.SetValueEx();
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

    #region Kill

    public void AddKill()
    {
        kill += 1;
    }

    #endregion
}
