using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinItemUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text value;
    [SerializeField] private Button btn;
    [SerializeField] private Text price;

    public void SetImage(Sprite sprite)
    {
        this.image.sprite = sprite;
    }

    public void SetValue(int value)
    {
        this.value.text = value.ToString();
    }
    
    public void SetPrice(int price)
    {
        this.price.text = price.ToString();
    }

    public void OnButtonBuy(int i, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(i));
    }
}
