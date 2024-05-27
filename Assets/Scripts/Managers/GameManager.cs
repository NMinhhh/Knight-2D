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

    public int stage {  get; private set; }


    //Time in game
    public int minutes { get; private set; }
    public int seconds { get; private set; }

    private float timer = 1;

    public int energy {  get; private set; }

    public int coin {  get; private set; }

    public int diamond;

    public int kill {  get; private set; }

    #endregion

    #region Unity Function
    
    private void Start()
    {
        CanvasManager.Instance.EnergyUIActive();
        energy = 999;
    }

    private void Update()
    {
        Timer();
     
    }


    public void StageLevelUp()
    {
        if (!SelectionSkill.Instance.isAllSkillFullLevel)
        {
            SelectionSkill.Instance.AppearSkill();
            MenuSkillUI.Instance.OpenMenuSkill();
            SelectionSkill.Instance.ResetItemUI();
        }
        stage++;
    }

    #endregion

    public int Calculate(float basicIndex, float levelupIndex, float levelupIndexPercent, int level)
    {
        return Mathf.FloorToInt((basicIndex + (levelupIndex * (level - 1))) * Mathf.Pow((1 + levelupIndexPercent / 100), (level - 1))); 
    }

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



    #region Coin 

    public void PickUpCoin(int amount)
    {
        coin += amount;
    }

    public void PickUpDiamond(int amount)
    {
        diamond += amount;
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
        return energy >= amount;
    }

    public void AddEnergy()
    {
        energy += 1;
    }

    public void UseEnergy(int amount)
    {
        energy -= amount;
    }

    #endregion
}
