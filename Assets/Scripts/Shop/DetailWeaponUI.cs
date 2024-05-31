using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailWeaponUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private Image image;
    [SerializeField] private Text indexText;
    [SerializeField] private Text infoText;

    public void SetNameText(string name)
    {
        this.nameText.text = name;
    }
    
    public void SetImage(Sprite image)
    {
        this.image.sprite = image;
    }

    public void SetIndexText(string indexText)
    {
        this.indexText.text = "";
        string[] text = indexText.Split(',');
        for (int i = 0; i < text.Length; i++)
        {
            this.indexText.text += text[i] + '\n';
        }
    }

    public void SetInfoText(string infoText)
    {
        this.infoText.text = infoText;
    }
}
