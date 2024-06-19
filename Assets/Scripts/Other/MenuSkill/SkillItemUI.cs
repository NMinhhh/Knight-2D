using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour
{
    [SerializeField] private Text nameText;
    [SerializeField] private GameObject levelImageObj;
    [SerializeField] private GameObject[] levelImages;
    [SerializeField] private Image image;
    [SerializeField] private Text infoText;
    [SerializeField] private Text indexText;
    [SerializeField] private Button btn;
    [SerializeField] private Outline outlineBtn;
    private int idSkill;
    private Animator anim;
    private int level;

    public void SetNameText(string nameText)
    {
        this.nameText.text = nameText;
    }

    public void SetLevelImage(int level)
    {
        this.level = level;
        anim = levelImageObj.GetComponent<Animator>();
        string isBoolName = "isL";
        for(int i = 0; i < this.level; i++)
        {
            levelImages[i].SetActive(true);
            anim.SetBool(isBoolName + $"{i+1}", false);
        }
        anim.SetBool(isBoolName + this.level.ToString(), true);
    }

    public void SetImage(Sprite image, Vector2 size)
    {
        this.image.sprite = image;
        RectTransform rect = this.image.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(size.x, size.y);
    }

    public void SetInfoSkill(string infoText, string[] indexText)
    {
        if(this.level > 1)
        {
            SetInfoText("");
            SetIndexText(indexText[this.level - 2]);
        }
        else
        {
            SetInfoText(infoText);
            SetIndexText("");
        }
    }
    public void SetInfoText(string infoText)
    {
        this.infoText.text = infoText;
    }

    public void SetIndexText(string indextext)
    {
        this.indexText.text = "";
        string[] text = indextext.Split(',');
        for(int i = 0; i < text.Length; i++)
        {
            this.indexText.text += text[i] + '\n';
        }
    }

    public void OnClickSelectionSkill(int idx, Action<int> action)
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => action(idx));
    }

    public void SetIdSkill(int id)
    {
        this.idSkill = id;
    }

    public int GetIdSkill()
    {
        return this.idSkill;
    }

    public void ChooseSkill()
    {
        outlineBtn.effectColor = new Color(outlineBtn.effectColor.r,outlineBtn.effectColor.g, outlineBtn.effectColor.b, 1);
    }

    public void UnChooseSkill()
    {
        outlineBtn.effectColor = new Color(outlineBtn.effectColor.r, outlineBtn.effectColor.g, outlineBtn.effectColor.b, 0);
    }
}
