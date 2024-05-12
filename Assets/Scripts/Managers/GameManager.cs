using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int stage {  get; private set; }

    public bool isLevelUp {  get; private set; }

    public int energy {  get; private set; }

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
        CanvasManager.Instance.EnergyUIActive();
        level = 1;
        isLevelUp = false;
        maxEx = CalculateExperience();
        exStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();

    }

    private void Update()
    {
        //if(currentEx >= maxEx && !isLevelUp)
        //{
        //    isLevelUp = true;
        //}

        //if(isLevelUp)
        //{
        //    isLevelUp = false;
        //    LevelUp();
        //}
        Timer();
     
    }


    public void StageLevelUp()
    {
        if (!SelectionSkill.Instance.isAllSkillFullLevel)
        {
            SelectionSkill.Instance.ResetEnergy();
            SelectionSkill.Instance.OpenMenuSkill();
            MenuSkillUI.Instance.OpenMenuSkill();
        }
        stage++;
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
            SelectionSkill.Instance.OpenMenuSkill();
            MenuSkillUI.Instance.OpenMenuSkill();
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

    #region Energy

    public bool HasEnoughEnergy(int amount)
    {
        return this.energy >= amount;
    }

    public void AddEnergy()
    {
        energy += 1;
    }

    public void UseEnergy(int amount)
    {
        this.energy -= amount;
    }

    #endregion
}
