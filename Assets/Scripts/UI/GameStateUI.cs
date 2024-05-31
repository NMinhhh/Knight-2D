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
    [SerializeField] private GameObject[] gameStateTitle;
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
        CloseLossUI();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCheckTimer)
        {
            Timer();
        }
    }

    public void OpenWinUI()
    {
        GameManager.Instance.AddDiamond(MapManager.Instance.diamond);
        SetPickUpValue();
        gameStateTitle[0].SetActive(true);
        loss.SetActive(true);
        Time.timeScale = 0;
        
    }

    public void OpenLossUI()
    {
        SetPickUpValue();
        gameStateTitle[1].SetActive(true);
        bornUI.SetActive(true);
        loss.SetActive(true);
        isCheckTimer = true;
        Time.timeScale = 0;
    }

    public void CloseLossUI()
    {
        foreach (GameObject item in gameStateTitle)
        {
            item.SetActive(false);
        }
        ResetBornUI();
        loss.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetPickUpValue()
    {
        coinText.text = MapManager.Instance.coin.ToString();
        diamondText.text = MapManager.Instance.diamond.ToString();
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
        GameManager.Instance.AddDiamond(MapManager.Instance.diamond);
        ResetBornUI();
        bornUI.SetActive(false);
    }

    void ResetBornUI()
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
