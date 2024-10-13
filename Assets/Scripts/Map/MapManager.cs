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
        SoundFXManager.Instance.Initialize();
    }
    #endregion

    #region Variable

    public int stage { get; private set; }

    public int energy { get; private set; }

    public int coin { get; private set; }

    public int diamond { get; private set; }

    public bool isBoss {  get; private set; }

    public bool isLose {  get; private set; }
    public bool isWin {  get; private set; }

    private int diamondPay;

    private Player player;

    #endregion

    #region Unity Function

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        diamondPay = 1;
        isWin = false;
        isLose = false;
    }

    private void Update()
    {
        if (player.isDie && !isLose && !isWin)
        {
            isLose = true;
            GameStateUI.Instance.SetDiamondToBorn(diamondPay);
            GameStateUI.Instance.ClickToBorn(Revive);
            GameStateUI.Instance.OpenLossUI();
        }
    }



    #endregion

    public void SetBossState(bool state)
    {
        isBoss = state;
    }

    public void Winner()
    {
        isWin = true;
    }

    public void Revive()
    {
        if (GameManager.Instance.HasEnenoughDiamond(diamondPay))
        {
            GameManager.Instance.UseDiamond(diamondPay);
            GameStateUI.Instance.CloseLossUI();
        }
        else
        {
            GameStateUI.Instance.NoEnoughDiamond();
            return;
        }
        diamondPay++;
        player.Born();
        isLose = false;
    }

    public void StageLevelUp()
    {
        stage++;
    }

    public void OpenOption()
    {
        if (!SelectionSkill.Instance.isAllSkillFullLevel)
        {
            SelectionSkill.Instance.AppearSkill();
            MenuSkillUI.Instance.OpenMenuSkill();
            SelectionSkill.Instance.ResetItemUI();
        }
    }

    public int Calculate(float basicIndex, float levelupIndex, float levelupIndexPercent, int level)
    {
        return Mathf.CeilToInt((basicIndex + (levelupIndex * (level - 1))) * Mathf.Pow((1 + levelupIndexPercent / 100), (level - 1)));
    }

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

    #region Energy

    public bool HasEnoughEnergy(int amount)
    {
        return energy >= amount;
    }

    public void AddEnergy(int amount)
    {
        energy += amount;
    }

    public void UseEnergy(int amount)
    {
        energy -= amount;
    }

    #endregion
}
