using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    [SerializeField] private int diamondPay;
    private bool isLoss;
    private int mutiply;

    private Player player;

    void Start()
    {
        mutiply = 1;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDie && !isLoss)
        {
            isLoss = true;
            GameStateUI.Instance.SetDiamondToBorn(diamondPay * mutiply);
            GameStateUI.Instance.ClickToBorn(Born);
            GameStateUI.Instance.OpenLossUI();
            Time.timeScale = 0;
        }
    }

    public void Born()
    {
        if(CoinManager.Instance.HasEnenoughDiamond(diamondPay * mutiply)) 
        {
            CoinManager.Instance.UseDiamond(diamondPay * mutiply);
            GameStateUI.Instance.CloseLossUI();
            GameStateUI.Instance.ResetBornUI();
        }
        else
        {
            Debug.Log("Ko du kim cuong");
            return;
        }
        Time.timeScale = 1;
        mutiply += 2;
        player.Born();
        isLoss = false;
    }
}
