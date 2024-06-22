using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStateUI : MonoBehaviour
{
    public static GameStateUI Instance {  get; private set; }
    [Header("Loss")]
    [SerializeField] private GameObject stateUI;
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;
    [SerializeField] private GameObject[] gameStateTitle;
    [Space]
    [Space]
    [Space]

    [Header("Born UI")]
    [SerializeField] private GameObject bornUI;
    [SerializeField] private Text diamondPayText;
    [SerializeField] private Button btnToBorn;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject noEnoughDiamond;
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
        CloseLoseUI();
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
        SoundFXManager.Instance.StopMusic();
        SetPickUpValue();
        gameStateTitle[0].SetActive(true);
        stateUI.SetActive(true);
        gameStateTitle[0].transform.GetChild(2).GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        
    }

    public void OpenLossUI()
    {
        SoundFXManager.Instance.StopMusic();
        gameStateTitle[1].SetActive(true);
        bornUI.SetActive(true);
        stateUI.SetActive(true);
        gameStateTitle[1].transform.GetChild(2).GetComponent<AudioSource>().Play();
        isCheckTimer = true;
        Time.timeScale = 0;
    }

    public void CloseLoseUI()
    {
        foreach (GameObject title in gameStateTitle)
        {
            title.SetActive(false);
            title.transform.GetChild(2).GetComponent<AudioSource>().Stop();
        }
        SoundFXManager.Instance.PlayMusic();
        ResetBornUI();
        stateUI.SetActive(false);
        Time.timeScale = 1;
    }

    private void SetPickUpValue()
    {
        coinText.text = MapManager.Instance.coin.ToString();
        diamondText.text = MapManager.Instance.diamond.ToString();
        GameManager.Instance.AddDiamond(MapManager.Instance.diamond);
        GameManager.Instance.AddCoin(MapManager.Instance.coin);
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
        SetPickUpValue();
        ResetBornUI();
    }

    public void NoEnoughDiamond()
    {
        noEnoughDiamond.SetActive(true);
    }

    void ResetBornUI()
    {
        isCheckTimer = false;
        second = 8;
        timeText.text = second.ToString();
        noEnoughDiamond.SetActive(false);
        bornUI.SetActive(false);
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
