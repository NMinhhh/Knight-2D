using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarItemUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text nameText;
    [SerializeField] private Button btn;
    [SerializeField] private Text priceText;
    [SerializeField] private GameObject backgroundIsPurchased;
    
    public int id {  get; private set; }

    public void SetImage(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetPrice(string price)
    {
        priceText.text = price;
    }

    public void OnAvatarUnlock(int i, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(i));
    }
    
    public void AvatarPurchased()
    {
        backgroundIsPurchased.SetActive(true);
        btn.gameObject.SetActive(false);
    }
    
    public void SetID(int id)
    {
        this.id = id;
    }
}
