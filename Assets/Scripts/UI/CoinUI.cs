using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    [SerializeField] private Text coinText;
    [SerializeField] private Text diamondText;

    [Range(0f, 1f)]
    [SerializeField] private float speed;

    private int coin;
    private int coinCurrent;

    private void Start()
    {
        coin = CoinManager.Instance.coin;
        coinCurrent = coin;
        coinText.text = coinCurrent.ToString();
    }

    void Update()
    {
        coin = CoinManager.Instance.coin;
        if (coinCurrent != coin)
        {
            StartCoroutine(Effect());
        }
        diamondText.text = CoinManager.Instance.diamond.ToString();
    }
    
    IEnumerator Effect()
    {
        yield return new WaitForSeconds(speed);
        if(coinCurrent != coin)
        {
            if (coinCurrent < coin)
                coinCurrent++;
            if (coinCurrent > coin)
                coinCurrent--;
            coinText.text = coinCurrent.ToString();
            StartCoroutine(Effect());
        }
    }
}
