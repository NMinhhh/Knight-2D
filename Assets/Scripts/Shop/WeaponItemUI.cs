using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponItemUI : MonoBehaviour
{
    [SerializeField] private Text nameWeapon;
    [SerializeField] private Image image;
    [SerializeField] private Text damageText;
    [SerializeField] private Text bullets;
    [SerializeField] private Text reloadText;
    [SerializeField] private Button button;
    [SerializeField] private Text price;

    public void SetWeaponName(string name)
    {
        nameWeapon.text = name;
    }

    public void SetWeaponImage(Sprite image, float widthImage)
    {
        this.image.sprite = image;
        RectTransform rect = this.image.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(widthImage, 314.8f);
    }

    public void SetWeaponDamage(string damageText)
    {
        this.damageText.text = damageText;
    }

    public void SetWeaponBullets(string bulletsText)
    {
        this.bullets.text = bulletsText;
    }

    public void SetWeaponReload(string reloadText)
    {
        this.reloadText.text = reloadText;
    }

    public void SetWeaponPrice(string priceText)
    {
        this.price.text = priceText;
    }

    public void OnWeaponUnlockButton(int idx, Action<int> action)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action(idx));
    }

    public void OnWeaponUnEquipButton(int idx, Action<int> action)
    {
        button.interactable = true;
        price.text = "Equip";
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => action(idx));
    }

    public void OnWeaponEquipButton()
    {
        button.interactable = false;
        price.text = "UnEquip";
    }
}

