using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;
    void Update()
    {
        coinText.text = CoinManager.Instance.coin.ToString();
        diamondText.text = CoinManager.Instance.diamond.ToString();
    }   
}
