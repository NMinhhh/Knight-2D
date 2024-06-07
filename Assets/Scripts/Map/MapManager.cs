using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    #region Instance

    public static MapManager Instance;

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

    public int stage { get; private set; }


    //Time in game
    public int minutes { get; private set; }
    public int seconds { get; private set; }

    private float timer = 1;

    public int energy;

    public int coin { get; private set; }

    public int diamond { get; private set; }

    public int kill { get; private set; }

    public bool isLoss {  get; private set; }

    private int diamondPay;

    private Player player;

    #endregion

    #region Unity Function

    private void Start()
    {
        CanvasManager.Instance.EnergyUIActive();
        player = GameObject.Find("Player").GetComponent<Player>();
        diamondPay = 1;
    }

    private void Update()
    {
        if (player.isDie && !isLoss)
        {
            isLoss = true;
            GameStateUI.Instance.SetDiamondToBorn(diamondPay);
            GameStateUI.Instance.ClickToBorn(Revive);
            GameStateUI.Instance.OpenLossUI();
        }
        Timer();
    }



    #endregion

    public void Revive()
    {
        if (GameManager.Instance.HasEnenoughDiamond(diamondPay))
        {
            GameManager.Instance.UseDiamond(diamondPay);
            GameStateUI.Instance.CloseLossUI();
        }
        else
        {
            Debug.Log("Ko du kim cuong");
            return;
        }
        diamondPay++;
        player.Born();
        isLoss = false;
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


    public int Calculate(float basicIndex, float levelupIndex, float levelupIndexPercent, int level)
    {
        return Mathf.CeilToInt((basicIndex + (levelupIndex * (level - 1))) * Mathf.Pow((1 + levelupIndexPercent / 100), (level - 1)));
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
