using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text priceText;
    [SerializeField] private Button btn;
    [SerializeField] private Outline outlineBtn;
    [SerializeField] private GameObject itemPurchasedBg;

    private string infoText;

    private int id;
   
    public void SetImage(Sprite sprite)
    {
        this.image.sprite = sprite;
    }

    public void SetPrice(string price)
    {
        this.priceText.text = price;
    }

    public void SetInfoItem(string infoText)
    {
        this.infoText = "";
        string[] text = infoText.Split(',');
        for (int i = 0; i < text.Length; i++)
        {
            this.infoText += text[i] + '\n';
        }
    }

    public string GetInfoText()
    {
        return infoText;
    }

    public void OnClickSelectedItem(int id, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(id));
    }

    public void ChooseItem()
    {
        outlineBtn.effectColor = new Color(outlineBtn.effectColor.r, outlineBtn.effectColor.g, outlineBtn.effectColor.b, 1);
    }

    public void UnChooseItem()
    {
        outlineBtn.effectColor = new Color(outlineBtn.effectColor.r, outlineBtn.effectColor.g, outlineBtn.effectColor.b, 0);
    }

    public void ResetItem()
    {
        btn.interactable = true;
        itemPurchasedBg.SetActive(false);
        UnChooseItem();
    }

    public void ItemPurchased()
    {
        btn.interactable = false;
        itemPurchasedBg.SetActive(true);
    }
}
