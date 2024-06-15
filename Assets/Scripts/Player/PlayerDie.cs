using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    private int diamondPay;
    private bool isLoss;

    private Player player;

    void Start()
    {
        diamondPay = 1;
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isDie && !isLoss)
        {
            isLoss = true;
            GameStateUI.Instance.SetDiamondToBorn(diamondPay);
            GameStateUI.Instance.ClickToBorn(Born);
            GameStateUI.Instance.OpenLossUI();
        }
    }

    public void Born()
    {
        if(GameManager.Instance.HasEnenoughDiamond(diamondPay)) 
        {
            GameManager.Instance.UseDiamond(diamondPay);
            GameStateUI.Instance.CloseLoseUI();
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
}
