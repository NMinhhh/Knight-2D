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
        }
    }

    public void Born()
    {
        if(GameManager.Instance.HasEnenoughDiamond(diamondPay * mutiply)) 
        {
            GameManager.Instance.UseDiamond(diamondPay * mutiply);
            GameStateUI.Instance.CloseLossUI();
        }
        else
        {
            Debug.Log("Ko du kim cuong");
            return;
        }
        mutiply += 2;
        player.Born();
        isLoss = false;
    }
}
