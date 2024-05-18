using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    public static GameStateUI Instance {  get; private set; }
    [Header("Loss")]
    [SerializeField] private GameObject loss;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;
    [Space]
    [Space]
    [Space]

    [Header("Born UI")]
    [SerializeField] private GameObject bornUI;
    [SerializeField] private Text diamondPayText;
    [SerializeField] private Button btnClose;
    [SerializeField] private Button btnToBorn;
    [SerializeField] private Text timeText;
    private bool isCheckTimer;
    private float timer = 1;
    private float second = 8;

    //[Header("Win")]
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        timeText.text = second.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        if (isCheckTimer)
        {
            Timer();
        }
    }

    public void OpenLossUI()
    {
        SetPickUpValue();
        bornUI.SetActive(true);
        loss.SetActive(true);
        isCheckTimer = true;
    }

    public void CloseLossUI()
    {
        loss.SetActive(false);
    }
    private void SetPickUpValue()
    {
        coinText.text = GameManager.Instance.coin.ToString();
        diamondText.text = GameManager.Instance.diamond.ToString();
    }


    public void SetDiamondToBorn(int amount)
    {
        diamondPayText.text = amount.ToString();
    }

    public void ClickToBorn(Action action)
    {
        btnToBorn.onClick.RemoveAllListeners();
        btnToBorn.onClick.AddListener(() => action());
    }

    public void ButtonClickBornUIClose()
    {
        CoinManager.Instance.AddDiamond(GameManager.Instance.diamond);
        ResetBornUI();
        bornUI.SetActive(false);
    }

    public void ResetBornUI()
    {
        isCheckTimer = false;
        second = 8;
        timeText.text = second.ToString();
    }

    private void Timer()
    {
        timer -= Time.unscaledDeltaTime;
        if(timer <= 0)
        {
            timer = 1;
            second--;
            timeText.text = second.ToString();
            if (second == 0)
            {
                ButtonClickBornUIClose();
            }
        }
    }
}
