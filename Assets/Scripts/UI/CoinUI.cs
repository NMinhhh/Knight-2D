using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    [SerializeField] private bool isGamePlay;

    private void Start()
    {
    }

    void Update()
    {
        if (isGamePlay)
        {
            GamePlay();
        }
        else
        {
            Home();
        }
    }
    
    void Home()
    {
        coinText.text = CoinManager.Instance.coin.ToString();
        diamondText.text = CoinManager.Instance.diamond.ToString();
    }

    void GamePlay()
    {
        coinText.text = GameManager.Instance.coin.ToString();
        diamondText.text = GameManager.Instance.diamond.ToString();
    }
}
